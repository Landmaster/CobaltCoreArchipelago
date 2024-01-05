using HarmonyLib;

namespace CobaltCoreArchipelago
{
    [HarmonyPatch(typeof(CardReward))]
    public class CardRewardPatch
    {
        public static event EventHandler<CardReward>? OnTakeCard;

        [HarmonyPatch(nameof(CardReward.TakeCard))]
        [HarmonyPostfix]
        public static void TakeCardPostfix(CardReward __instance, G g, Card card)
        {
            OnTakeCard?.Invoke(typeof(CardRewardPatch), __instance);
        }
    }
}
