using HarmonyLib;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CobaltCoreArchipelago
{
    [HarmonyPatch(typeof(Combat))]
    class CombatPatch
    {
        [HarmonyPatch("PlayerWon")]
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> PlayerWonTranspile(IEnumerable<CodeInstruction> instructions) {
            //CCArchiManifest.Instance!.Logger!.LogWarning("hahaha");
            var aCardOfferingCtor = typeof(ACardOffering).GetConstructor(Array.Empty<Type>());
            foreach (var instruction in instructions)
            {
                if (aCardOfferingCtor!.Equals(instruction.operand)) {
                    CCArchiManifest.Instance!.Logger!.LogWarning("hahaha");
                }
            }
            return instructions;
        }
    }
}
