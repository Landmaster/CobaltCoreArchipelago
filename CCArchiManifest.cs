using CobaltCoreModding.Definitions;
using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using CobaltCoreModding.Definitions.ModManifests;
using HarmonyLib;
using Microsoft.Extensions.Logging;

namespace CobaltCoreArchipelago
{
    public sealed class CCArchiManifest : IModManifest, ISpriteManifest
    {
        public IEnumerable<DependencyEntry> Dependencies => Array.Empty<DependencyEntry>();

        public DirectoryInfo? GameRootFolder { get; set; }
        public ILogger? Logger { get; set; }
        public DirectoryInfo? ModRootFolder { get; set; }

        public string Name => "Landmaster.CobaltCoreArchipelago.MainManifest";

        public void BootMod(IModLoaderContact contact)
        {
            //Logger?.LogWarning("Ow! But my face, though!");
            var harmony = new Harmony("Landmaster.CobaltCoreArchipelago.Patch");
            harmony.PatchAll();
        }

        public void LoadManifest(ISpriteRegistry artRegistry)
        {
            if (ModRootFolder == null) {
                throw new Exception("No root folder set!");
            }
            var path = Path.Combine(ModRootFolder.FullName, Path.GetFileName("archi_icon_cobaltcore.png"));
            CCArchiSprites.ArchiIcon = new ExternalSprite("Landmaster.CobaltCoreArchipelago.ButtonMap", new FileInfo(path));
            if (!artRegistry.RegisterArt(CCArchiSprites.ArchiIcon /*, (int) Spr.buttons_deck*/)) {
                throw new Exception("Cannot register sprite.");
            }   
        }
    }

    [HarmonyPatch(typeof(CornerMenu), nameof(CornerMenu.Render))]
    class CornerMenuPatch {
        static void Postfix(G g, State s) {
            Draw.Sprite((Spr)CCArchiSprites.ArchiIcon!.Id!, 1, 20);
        }
    }
}
