namespace CobaltCoreArchipelago
{
    public class ArchiArtifactReward : ArtifactReward
    {
        public UK UkValue { get; set; }

        public void OnTakeArtifact()
        {
            CCArchiData.Instance.RedeemedItemIds.Add(UkValue);
            // advance the rng
            for (int i = 0; i < artifacts.Count; ++i) {
                CCArchiData.Instance.ArchiArtifactOfferingRand.Next();
            }
        }
    }
}
