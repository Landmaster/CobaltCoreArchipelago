﻿using HarmonyLib;

namespace CobaltCoreArchipelago
{
    [HarmonyPatch(typeof(CornerMenu), nameof(CornerMenu.Render))]
    class CornerMenuPatch {
        static void Postfix(G g, State s) {
            var rect = new Rect(1, 20, 15, 15);
            var box = g.Push(key: ArchiUKs.ArchiButton, rect: rect, onMouseDown: new MouseDownHandler());
            if (box.IsHover()) {
                g.tooltips.AddText(box.rect.xy + new Vec(15.0, 15.0), "Archipelago");
            }
            Draw.Sprite((Spr)CCArchiSprites.ArchiIcon!.Id!, rect.x, rect.y);
            g.Pop();
        }

        class MouseDownHandler : OnMouseDown
        {
            public void OnMouseDown(G g, Box b)
            {
                // TODO add stuff
            }
        }
    }
}
