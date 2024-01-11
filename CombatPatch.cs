using HarmonyLib;
using System.Reflection.Emit;

namespace CobaltCoreArchipelago
{
    [HarmonyPatch(typeof(Combat))]
    class CombatPatch
    {
        [HarmonyPatch("PlayerWon")]
        [HarmonyTranspiler]
        public static IEnumerable<CodeInstruction> PlayerWonTranspile(IEnumerable<CodeInstruction> instructions) {
            var aCardOfferingCtor = typeof(ACardOffering).GetConstructor(Array.Empty<Type>());
            var aArtifactOfferingCtor = typeof(AArtifactOffering).GetConstructor(Array.Empty<Type>());
            foreach (var instruction in instructions)
            {
                if (instruction.opcode.Equals(OpCodes.Newobj) && aCardOfferingCtor!.Equals(instruction.operand))
                {
                    yield return new CodeInstruction(
                        OpCodes.Newobj,
                        typeof(AArchiCardOffering).GetConstructor(Array.Empty<Type>())
                    );
                }
                else if (instruction.opcode.Equals(OpCodes.Newobj) && aArtifactOfferingCtor!.Equals(instruction.operand)) {
                    yield return new CodeInstruction(
                        OpCodes.Newobj,
                        typeof(AArchiArtifactOffering).GetConstructor(Array.Empty<Type>())
                    );
                }
                else
                {
                    yield return instruction;
                }
            }
        }
    }
}
