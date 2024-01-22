using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using CobaltCoreModding.Definitions;
using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using CobaltCoreModding.Definitions.ModManifests;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Reflection;

namespace CobaltCoreArchipelago
{
    public sealed class CCArchiManifest : IModManifest, ISpriteManifest
    {
        public IEnumerable<DependencyEntry> Dependencies => Array.Empty<DependencyEntry>();

        public DirectoryInfo? GameRootFolder { get; set; }
        public ILogger? Logger { get; set; }
        public DirectoryInfo? ModRootFolder { get; set; }

        public static CCArchiManifest? Instance { get; private set; }

        public string Name => "Landmaster.CobaltCoreArchipelago.MainManifest";

        public void BootMod(IModLoaderContact contact)
        {
            Instance = this;

            var harmony = new Harmony("Landmaster.CobaltCoreArchipelago.Patch");
            harmony.PatchAll();

            var cornersField = typeof(G).GetField("corners", BindingFlags.Static | BindingFlags.NonPublic);
            var cornersFieldVal = cornersField!.GetValue(null) as HashSet<UK>;
            cornersFieldVal!.Add(ArchiUKs.ArchiButton);

            var config = JsonConvert.DeserializeObject<CCArchiConfig>(
                File.ReadAllText(Path.Combine(ModRootFolder!.FullName, "CobaltCoreArchipelago.config.json"))
            )!;

            CCArchiData.Session = ArchipelagoSessionFactory.CreateSession(config.ArchipelagoUrl);
            var loginResult = CCArchiData.Session.TryConnectAndLogin(
                "Cobalt Core", config.ArchipelagoUsername, ItemsHandlingFlags.AllItems, password: config.ArchipelagoPassword
            );

            if (!loginResult.Successful)
            {
                var loginFailure = (LoginFailure)loginResult;
                var errorMessage = "Failed to connect to Archipelago server:\n";
                foreach (var error in loginFailure.Errors)
                {
                    errorMessage += error + "\n";
                }
                throw new Exception(errorMessage);
            }

            var loginSuccess = (LoginSuccessful)loginResult;
            CCArchiData.SlotData = loginSuccess.SlotData;
        }

        public void LoadManifest(ISpriteRegistry artRegistry)
        {
            if (ModRootFolder == null) {
                throw new Exception("No root folder set!");
            }

            var path = Path.Combine(ModRootFolder.FullName, "archi_icon_cobaltcore.png");
            CCArchiSprites.ArchiIcon = new ExternalSprite("Landmaster.CobaltCoreArchipelago.ArchiIcon", new FileInfo(path));
            if (!artRegistry.RegisterArt(CCArchiSprites.ArchiIcon)) {
                throw new Exception("Cannot register sprite.");
            }

            path = Path.Combine(ModRootFolder.FullName, "archi_icon_selected_cobaltcore.png");
            CCArchiSprites.ArchiIconSelected = new ExternalSprite("Landmaster.CobaltCoreArchipelago.ArchiIconSelected", new FileInfo(path));
            if (!artRegistry.RegisterArt(CCArchiSprites.ArchiIconSelected))
            {
                throw new Exception("Cannot register sprite.");
            }

            path = Path.Combine(ModRootFolder.FullName, "archi_icon_large_cobaltcore.png");
            CCArchiSprites.ArchiIconLarge = new ExternalSprite("Landmaster.CobaltCoreArchipelago.ArchiIconLarge", new FileInfo(path));
            if (!artRegistry.RegisterArt(CCArchiSprites.ArchiIconLarge))
            {
                throw new Exception("Cannot register sprite.");
            }
        }
    }
}
