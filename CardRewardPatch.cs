using HarmonyLib;

namespace CobaltCoreArchipelago
{
    [HarmonyPatch(typeof(CardReward))]
    class CardRewardPatch
    {
        [HarmonyPatch(nameof(CardReward.TakeCard))]
        [HarmonyPostfix]
        public static void TakeCardPostfix(CardReward __instance, G g, Card card)
        {
            if (__instance is ArchiCardReward reward) {
                reward.OnTakeCard();
            }
        }
    }
}
