using HarmonyLib;

namespace CobaltCoreArchipelago
{
    [HarmonyPatch(typeof(Loc), nameof(Loc.T), new Type[] { typeof(string), typeof(string) })]
    class LocPatch
    {
        static void Postfix(ref string __result, string key, string _) {
            if (key.Equals("mainMenu.play") || key.Equals("mainMenu.continue")) {
                __result = "Archipelago";
            }
        }
    }
}
