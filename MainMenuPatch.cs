using HarmonyLib;
using Microsoft.Extensions.Logging;

namespace CobaltCoreArchipelago
{
    [HarmonyPatch(typeof(MainMenu))]
    class MainMenuPatch
    {
        [HarmonyPatch(nameof(MainMenu.OnMouseDown))]
        [HarmonyPostfix]
        static void OnMouseDownPostfix(G g, Box b) {
            if (b.key == UK.menu_play) {
                var slotData = CCArchiData.SlotData!;

                g.state.PopulateRun(
                    CCArchiData.StarterShipFromId((long) slotData["ship"])!,
                    chars: new Deck[] {
                        (Deck) (int) (long) slotData["character1"],
                        (Deck) (int) (long) slotData["character2"],
                        (Deck) (int) (long) slotData["character3"]
                    },
                    difficulty: (int) (long) slotData["difficulty"],
                    seed: 5908243,
                    giveRunStartRewards: true
                );

            }
        }
    }
}
