using HarmonyLib;
using System.Reflection.Emit;

namespace CobaltCoreArchipelago
{
    [HarmonyPatch(typeof(ArtifactReward))]
    public class ArtifactRewardPatch
    {
        public static void OnTakeArtifact(ArtifactReward reward) {
            if (reward is ArchiArtifactReward archiArtifactReward)
            {
                archiArtifactReward.OnTakeArtifact();
            }
        }

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(ArtifactReward.OnMouseDown))]
        public static IEnumerable<CodeInstruction> OnMouseDownTranspiler(IEnumerable<CodeInstruction> instructions) {
            var sendArtifactToCharMethod = typeof(State).GetMethod(nameof(State.SendArtifactToChar));
            var onTakeArtifactMethod = typeof(ArtifactRewardPatch).GetMethod(nameof(OnTakeArtifact));

            foreach (var instruction in instructions)
            {
                if (instruction.Calls(sendArtifactToCharMethod))
                {
                    yield return instruction;
                    // ArtifactRewardPatch.OnTakeArtifact(this);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, onTakeArtifactMethod);
                }
                else
                {
                    yield return instruction;
                }
            }
        }
    }
}
