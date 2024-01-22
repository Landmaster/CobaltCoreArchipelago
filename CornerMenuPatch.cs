using HarmonyLib;

namespace CobaltCoreArchipelago
{
    [HarmonyPatch(typeof(CornerMenu))]
    class CornerMenuPatch {
        [HarmonyPatch(nameof(CornerMenu.Render))]
        [HarmonyPostfix]
        static void RenderPostfix(G g, State s) {
            var rect = new Rect(1, 20, 15, 15);
            var box = g.Push(key: ArchiUKs.ArchiButton, rect: rect, onMouseDown: new MouseDownHandler());
            if (box.IsHover()) {
                g.tooltips.AddText(box.rect.xy + new Vec(15.0, 15.0), "Archipelago");
            }
            Draw.Sprite(
                (Spr)(g.state.routeOverride is CCArchiRoute ? CCArchiSprites.ArchiIconSelected!.Id! : CCArchiSprites.ArchiIcon!.Id!),
                rect.x, rect.y
            );
            Draw.Text(
                (CCArchiData.Session!.Items.AllItemsReceived.Count - CCArchiData.Instance.RedeemedItemIds.Count).ToString(),
                rect.x + 10, rect.y + 10, color: Colors.white
            );
            g.Pop();
        }

        class MouseDownHandler : OnMouseDown
        {
            public void OnMouseDown(G g, Box b)
            {
                if (g.state.routeOverride is CCArchiRoute)
                {
                    g.state.routeOverride = null;
                }
                else
                {
                    g.state.routeOverride = new CCArchiRoute();
                }
            }
        }
    }
}
