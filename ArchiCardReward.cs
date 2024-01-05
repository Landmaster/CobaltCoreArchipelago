namespace CobaltCoreArchipelago
{
    public class ArchiCardReward : CardReward
    {
        public int Idx { get; set; }

        public void OnTakeCard() {
            CCArchiData.Instance.RedeemedItemIds.Add(ArchiUKs.CardRewardUK(Idx));
        }
    }
}
