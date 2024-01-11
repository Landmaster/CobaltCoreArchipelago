namespace CobaltCoreArchipelago
{
    public class ArchiCardReward : CardReward
    {
        public UK UkValue { get; set; }

        public void OnTakeCard() {
            CCArchiData.Instance.RedeemedItemIds.Add(UkValue);
        }
    }
}
