using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CobaltCoreArchipelago
{
    class AArchiOffering : CardAction
    {
        public BattleType? BattleType;

        public override Route? BeginWithRoute(G g, State s, Combat c)
        {
            CCArchiManifest.Instance!.Logger!.LogWarning($"{BattleType!}");

            long theLocationId;

            if (BattleType == global::BattleType.Boss)
            {
                theLocationId = ArchiUKs.UKToArchiLocation(
                    CCArchiData.Instance.BossCount > 0 ? ArchiUKs.RareCardReward2UK : ArchiUKs.RareCardReward1UK
                );

                // TODO increment boss count at the right spot
            }
            else {
                theLocationId = ArchiUKs.UKToArchiLocation(
                    ArchiUKs.CardRewardUK(CCArchiData.Instance.CardDrawCount++)
                );
            }

            return new ArchiRewardRoute() {
                locationId = theLocationId
            };
        }

        class ArchiRewardRoute : Route, OnMouseDown {
            public long locationId = -1;

            [JsonIgnore]
            private bool first = true;

            public void OnMouseDown(G g, Box b)
            {
                // TODO add shit here
            }

            public override void Render(G g)
            {
                if (first) {
                    first = false;
                    if (locationId >= 0)
                    {
                        CCArchiData.Session!.Locations.CompleteLocationChecks(new long[] { locationId });
                    }
                }

                SharedArt.DrawEngineering(g);
                
                Draw.Text("Archipelago Reward", 240, 44, font: DB.stapler, color: Colors.textMain, align: daisyowl.text.TAlign.Center);
                Draw.Sprite((Spr?)CCArchiSprites.ArchiIconLarge!.Id, 240 - 128/2, 80);
                // TODO add more stuff
            }
        }
    }
}
