using HarmonyLib;

namespace CobaltCoreArchipelago
{
    [HarmonyPatch(typeof(NewRunOptions))]
    class NewRunOptionsPatch
    {
        /*
        [HarmonyPatch(nameof(NewRunOptions.OnEnter))]
        [HarmonyPostfix]
        static void OnEnterPostfix(State s) {
            CCArchiManifest.Instance!.Logger!.LogWarning("bwahahahaha");
        }*/
    }
}
