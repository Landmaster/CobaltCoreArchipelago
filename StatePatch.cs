using HarmonyLib;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CobaltCoreArchipelago
{
    [HarmonyPatch(typeof(State))]
    class StatePatch
    {
        [HarmonyPatch(nameof(State.PopulateRun))]
        [HarmonyPostfix]
        public static void PopulateRunPostfix()
        {
            CCArchiData.Instance.CardDrawCount = 0;
        }

        [HarmonyPatch(nameof(State.Save))]
        [HarmonyPostfix]
        public static void SavePostfix(State __instance) {
            int? slot = __instance.slot;
            if (!slot.HasValue)
            {
                return;
            }
            var archiDataPath = Path.Combine(State.GetSlotPath(slot.GetValueOrDefault()), "ArchiMWData.json");
            Storage.Save(CCArchiData.Instance, archiDataPath);
        }

        [HarmonyPatch(nameof(State.Load))]
        [HarmonyPostfix]
        public static void LoadPostfix(int slot)
        {
            var archiDataPath = Path.Combine(State.GetSlotPath(slot), "ArchiMWData.json");
            CCArchiData.Instance = Storage.LoadOrNew<CCArchiData>(archiDataPath);
        }
    }
}
