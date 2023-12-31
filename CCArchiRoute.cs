namespace CobaltCoreArchipelago
{
    public class CCArchiRoute : Route
    {
        public override void Render(G g)
        {
            SharedArt.DrawEngineering(g);

            Draw.Text("Archipelago", 240, 24, font: DB.stapler, color: Colors.textMain, align: daisyowl.text.TAlign.Center);

            var width = 120;

            g.Push(rect: new Rect(120 - width / 2.0, 50));
            for (int i = 0; i < 10; ++i) {
                SharedArt.MenuItem(g, new Vec(0, 0 + 21 * i), width, false, ArchiUKs.CardRewardUK(i), "Card Draw " + (i+1));
            }
            g.Pop();

            g.Push(rect: new Rect(240 - width / 2.0, 50));
            for (int i = 0; i<7; ++i)
            {
                SharedArt.MenuItem(g, new Vec(0, 0 + 21 * i), width, false, ArchiUKs.ArtifactUK(i), "Artifact " + (i + 1));
            }
            g.Pop();

            // TODO add more stuff
        }
    }
}
