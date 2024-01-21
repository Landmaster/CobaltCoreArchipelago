using Microsoft.Extensions.Logging;

namespace CobaltCoreArchipelago
{
    public class AArchiCardOffering : ACardOffering
    {
        public override Route? BeginWithRoute(G g, State s, Combat c)
        {
            CCArchiManifest.Instance!.Logger!.LogWarning($"{battleType!}");

            long theLocationId;

            if (battleType == BattleType.Boss)
            {
                theLocationId = ArchiUKs.UKToArchiLocation(
                    CCArchiData.Instance.RareCardDrawCount++ > 0 ? ArchiUKs.RareCardReward2UK : ArchiUKs.RareCardReward1UK
                );
            }
            else {
                int cardRewardIndex = CCArchiData.Instance.CardDrawCount / 2;

                if (CCArchiData.Instance.CardDrawCount++ % 2 == 0 || cardRewardIndex >= CCArchiData.NumCardRewards) {
                    return base.BeginWithRoute(g, s, c);
                }

                theLocationId = ArchiUKs.UKToArchiLocation(
                    ArchiUKs.CardRewardUK(cardRewardIndex)
                );
            }

            return new ArchiRewardRoute() {
                locationId = theLocationId
            };
        }
    }
}
