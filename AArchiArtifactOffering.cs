namespace CobaltCoreArchipelago
{
    public class AArchiArtifactOffering : AArtifactOffering
    {
        public override Route? BeginWithRoute(G g, State s, Combat c) {
            long theLocationId = -1;

            foreach (var pool in limitPools ?? Enumerable.Empty<ArtifactPool>()) {
                if (pool == ArtifactPool.Boss)
                {
                    theLocationId = ArchiUKs.UKToArchiLocation(
                        CCArchiData.Instance.BossArtifactCount++ > 0 ? ArchiUKs.BossArtifact2UK : ArchiUKs.BossArtifact1UK
                    );
                    break;
                }
            }

            if (theLocationId < 0 && CCArchiData.Instance.ArtifactCount < CCArchiData.NumArtifacts)
            {
                theLocationId = ArchiUKs.UKToArchiLocation(ArchiUKs.ArtifactUK(CCArchiData.Instance.ArtifactCount++));
            }

            return new ArchiRewardRoute() { 
                locationId = theLocationId
            };
        }
    }
}
