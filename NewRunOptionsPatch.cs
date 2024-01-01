using HarmonyLib;

namespace CobaltCoreArchipelago
{
    [HarmonyPatch(typeof(NewRunOptions))]
    class NewRunOptionsPatch
    {
        [HarmonyPatch(nameof(NewRunOptions.Render))]
        [HarmonyPostfix]
        static void OnEnterPostfix(G g) {
            var slotData = CCArchiData.SlotData!;

            g.state.PopulateRun(
                CCArchiData.StarterShipFromId((long)slotData["ship"])!,
                chars: new Deck[] {
                        (Deck) (int) (long) slotData["character1"],
                        (Deck) (int) (long) slotData["character2"],
                        (Deck) (int) (long) slotData["character3"]
                },
                difficulty: (int)(long)slotData["difficulty"],
                seed: (uint)slotData["seed"].GetHashCode(),
                giveRunStartRewards: true
            );
        }
    }
}
