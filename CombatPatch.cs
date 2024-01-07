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
            foreach (var instruction in instructions)
            {
                if (instruction.opcode.Equals(OpCodes.Newobj) && aCardOfferingCtor!.Equals(instruction.operand))
                {
                    yield return new CodeInstruction(
                        OpCodes.Newobj,
                        typeof(AArchiOffering).GetConstructor(Array.Empty<Type>())
                    );
                }
                else if (instruction.StoresField(typeof(ACardOffering).GetField("battleType"))) {
                    yield return new CodeInstruction(OpCodes.Stfld, typeof(AArchiOffering).GetField("BattleType"));
                }
                else
                {
                    yield return instruction;
                }
            }
        }
    }
}
