using CobaltCoreModding.Definitions;
using CobaltCoreModding.Definitions.ExternalItems;
using CobaltCoreModding.Definitions.ModContactPoints;
using CobaltCoreModding.Definitions.ModManifests;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using System.Reflection;

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

            var cornersField = typeof(G).GetField("corners", BindingFlags.Static | BindingFlags.NonPublic);
            var cornersFieldVal = cornersField!.GetValue(null) as HashSet<UK>;
            cornersFieldVal!.Add((UK)49201);
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

    class MouseDownHandler : global::OnMouseDown {

        public void OnMouseDown(G g, Box b)
        {
        }
    }

    [HarmonyPatch(typeof(CornerMenu), nameof(CornerMenu.Render))]
    class CornerMenuPatch {
        static void Postfix(G g, State s) {
            var rect = new Rect(1, 20, 15, 15);
            var box = g.Push(key: (UK)49201, rect: rect, onMouseDown: new MouseDownHandler());
            if (box.IsHover()) {
                g.tooltips.AddText(box.rect.xy + new Vec(15.0, 15.0), "Archipelago");
            }
            Draw.Sprite((Spr)CCArchiSprites.ArchiIcon!.Id!, rect.x, rect.y);
            g.Pop();
        }

        
    }
}
