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
                if (CCArchiData.Instance.CardDrawCount++ % 2 == 0) {
                    return base.BeginWithRoute(g, s, c);
                }

                theLocationId = ArchiUKs.UKToArchiLocation(
                    ArchiUKs.CardRewardUK((CCArchiData.Instance.CardDrawCount - 1) / 2)
                );
            }

            return new ArchiRewardRoute() {
                locationId = theLocationId
            };
        }
    }
}
