namespace CobaltCoreArchipelago
{
    public class CCArchiRoute : Route
    {
        public override void Render(G g)
        {
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
                SharedArt.MenuItem(g, new Vec(0, 0 + 21 * i), width, false, ArchiUKs.CardRewardUK(i), "Card Draw " + (i+1));
            }
            g.Pop();

            g.Push(rect: new Rect(240 - width / 2.0, 50));
            for (int i = 0; i < numArtifacts; ++i)
            {
                SharedArt.MenuItem(g, new Vec(0, 0 + 21 * i), width, false, ArchiUKs.ArtifactUK(i), "Artifact " + (i + 1));
            }
            g.Pop();

            g.Push(rect: new Rect(360 - width / 2.0, 50));
            var newWidth = 150;
            if (numRareCardDraws >= 1) SharedArt.MenuItem(g, new Vec(0, 0), newWidth, false, ArchiUKs.RareCardReward1UK, "Rare Card Draw 1");
            if (numBossArtifacts >= 1) SharedArt.MenuItem(g, new Vec(0, 21), newWidth, false, ArchiUKs.BossArtifact1UK, "Boss Artifact 1");
            if (numRareCardDraws >= 2) SharedArt.MenuItem(g, new Vec(0, 42), newWidth, false, ArchiUKs.RareCardReward2UK, "Rare Card Draw 2");
            if (numBossArtifacts >= 2) SharedArt.MenuItem(g, new Vec(0, 63), newWidth, false, ArchiUKs.BossArtifact2UK, "Boss Artifact 2");
            g.Pop();

            // TODO add more stuff
        }
    }
}
