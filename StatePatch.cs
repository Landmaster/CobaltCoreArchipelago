﻿using HarmonyLib;
using System.Reflection.Emit;

namespace CobaltCoreArchipelago
{
    [HarmonyPatch(typeof(State))]
    class StatePatch
    {
        [HarmonyPatch(nameof(State.PopulateRun))]
        [HarmonyPostfix]
        public static void PopulateRunPostfix()
        {
            //CCArchiData.Instance = new();

            CCArchiData.Session!.Locations.ScoutLocationsAsync(
                ArchiUKs.AllUKs().Select(ArchiUKs.UKToArchiLocation).ToArray()
            ).ContinueWith(packetTask => {
                foreach (var entry in packetTask.Result.Locations) {
                    var itemName = CCArchiData.Session!.Items.GetItemName(entry.Item);
                    var itemPlayer = CCArchiData.Session!.Players.GetPlayerName(entry.Player);
                    CCArchiData.Instance.LocationToItem[entry.Location] = $"{itemName} - {itemPlayer}";
                }
            });
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

        [HarmonyPatch(nameof(State.SeedRand))]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> SeedRandTranspiler(IEnumerable<CodeInstruction> instructions) { 
            foreach (var instruction in instructions)
            {
                yield return instruction;
            }
            yield return new CodeInstruction(OpCodes.Ldarg_0); // state
            yield return new CodeInstruction(OpCodes.Ldloc_0); // rand
            yield return new CodeInstruction(OpCodes.Call, typeof(StatePatch).GetMethod(nameof(AddRands)));
        }

        public static void AddRands(State state, Rand rand) {
            CCArchiData.Instance = new();

            CCArchiData.Instance.ArchiArtifactOfferingRand.seed = rand.Offshoot().seed;
            CCArchiData.Instance.ArchiBossArtifactOfferingRand.seed = rand.Offshoot().seed;
            var cardOfferingRand = state.rngCardOfferings;
            state.rngCardOfferings = rand.Offshoot();

            for (int i = 0; i < CCArchiData.NumCardRewards; ++i)
            {
                CCArchiData.Instance.ArchiCardChoices.Add(CardReward.GetOffering(state, count: 3));
            }

            for (int i = 0; i < 2; ++i)
            {
                CCArchiData.Instance.ArchiRareCardChoices.Add(CardReward.GetOffering(state, count: 3, battleType: BattleType.Boss));
            }

            state.rngCardOfferings = cardOfferingRand;
        }
    }
}
