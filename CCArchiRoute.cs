namespace CobaltCoreArchipelago
{
    public class CCArchiRoute : Route
    {
        private Route? SubRoute;

        public override void Render(G g)
        {
            if (SubRoute != null)
            {
                SubRoute.Render(g);
                return;
            }

            int numCardDraws = 0;
            int numRareCardDraws = 0;
            int numArtifacts = 0;
            int numBossArtifacts = 0;

            foreach (var item in CCArchiData.Session!.Items.AllItemsReceived) {
                switch (item.Item) {
                    case 250500:
                        numCardDraws++;
                        break;
                    case 250501:
                        numRareCardDraws++;
                        break;
                    case 250502:
                        numArtifacts++;
                        break;
                    case 250503:
                        numBossArtifacts++;
                        break;
                }
            }

            SharedArt.DrawEngineering(g);

            Draw.Text("Archipelago", 240, 24, font: DB.stapler, color: Colors.textMain, align: daisyowl.text.TAlign.Center);

            var width = 120;

            g.Push(rect: new Rect(120 - width / 2.0, 50));
            for (int i = 0; i < numCardDraws; ++i) {
                MenuItemIfUnredeemed(g, new Vec(0, 0 + 21 * i), width, false, ArchiUKs.CardRewardUK(i), "Card Draw " + (i + 1), new OnCardDrawClick() {
                    Idx = i,
                    ParentRoute = this
                });
            }
            g.Pop();

            g.Push(rect: new Rect(240 - width / 2.0, 50));
            for (int i = 0; i < numArtifacts; ++i)
            {
                MenuItemIfUnredeemed(g, new Vec(0, 0 + 21 * i), width, false, ArchiUKs.ArtifactUK(i), "Artifact " + (i + 1), new OnArtifactClick() { 
                    Idx = i,
                    ParentRoute = this
                });
            }
            g.Pop();

            g.Push(rect: new Rect(360 - width / 2.0, 50));
            var newWidth = 150;
            if (numRareCardDraws >= 1) MenuItemIfUnredeemed(g, new Vec(0, 0), newWidth, false, ArchiUKs.RareCardReward1UK, "Rare Card Draw 1", new OnRareCardDrawClick() { Idx = 0 });
            if (numBossArtifacts >= 1) MenuItemIfUnredeemed(g, new Vec(0, 21), newWidth, false, ArchiUKs.BossArtifact1UK, "Boss Artifact 1");
            if (numRareCardDraws >= 2) MenuItemIfUnredeemed(g, new Vec(0, 42), newWidth, false, ArchiUKs.RareCardReward2UK, "Rare Card Draw 2", new OnRareCardDrawClick() { Idx = 1 });
            if (numBossArtifacts >= 2) MenuItemIfUnredeemed(g, new Vec(0, 63), newWidth, false, ArchiUKs.BossArtifact2UK, "Boss Artifact 2");
            g.Pop();
        }

        public override bool TryCloseSubRoute(G g, Route r, object? arg)
        {
            if (r == SubRoute) {
                SubRoute = null;
                return true;
            }
            return SubRoute?.TryCloseSubRoute(g, r, arg) ?? false;
        }

        private static void MenuItemIfUnredeemed(G g, Vec v, int width, bool isBig, UIKey key, string name, OnMouseDown? onMouseDown = null) {
            if (!CCArchiData.Instance.RedeemedItemIds.Contains(key.k)) { 
                SharedArt.MenuItem(g, v, width, isBig, key, name, onMouseDown);
            }
        }

        private class OnCardDrawClick : OnMouseDown
        {
            public int Idx;
            public CCArchiRoute? ParentRoute;

            public void OnMouseDown(G g, Box b)
            {
                ParentRoute!.SubRoute = new ArchiCardReward() {
                    UkValue = ArchiUKs.CardRewardUK(Idx),
                    cards = CCArchiData.Instance.ArchiCardChoices[Idx]
                };
            }
        }

        private class OnRareCardDrawClick : OnMouseDown {
            public int Idx;
            public CCArchiRoute? ParentRoute;

            public void OnMouseDown(G g, Box b)
            {
                ParentRoute!.SubRoute = new ArchiCardReward()
                {
                    UkValue = Idx > 0 ? ArchiUKs.RareCardReward2UK : ArchiUKs.RareCardReward1UK,
                    cards = CCArchiData.Instance.ArchiRareCardChoices[Idx]
                };
            }
        }

        private class OnArtifactClick : OnMouseDown {
            public int Idx;
            public CCArchiRoute? ParentRoute;

            public void OnMouseDown(G g, Box b)
            {
                var rand = new Rand(CCArchiData.Instance.ArchiArtifactOfferingRand.seed);
                ParentRoute!.SubRoute = new ArchiArtifactReward()
                {
                    UkValue = ArchiUKs.ArtifactUK(Idx),
                    artifacts = ArtifactReward.GetOffering(g.state, 2, rngOverride: rand)
                };
            }
        }
    }
}
