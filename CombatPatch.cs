using HarmonyLib;
using Microsoft.Extensions.Logging;
using System.Reflection.Emit;

namespace CobaltCoreArchipelago
{
    [HarmonyPatch(typeof(Combat))]
    class CombatPatch
    {
        [HarmonyPatch("PlayerWon")]
        [HarmonyTranspiler]
        static IEnumerable<CodeInstruction> PlayerWonTranspile(IEnumerable<CodeInstruction> instructions) {
            var instructionsArr = instructions.ToArray();
            var aCardOfferingCtor = typeof(ACardOffering).GetConstructor(Array.Empty<Type>());
            foreach (var instruction in instructions)
            {
                if (instruction.opcode.Equals(OpCodes.Newobj) && aCardOfferingCtor!.Equals(instruction.operand))
                {
                    yield return new CodeInstruction(
                        OpCodes.Newobj,
                        typeof(AArchiOffering).GetConstructor(Array.Empty<Type>())
                    );
                }
                else { 
                    yield return instruction;
                }
            }
        }
    }
}
