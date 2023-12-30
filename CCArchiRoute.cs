namespace CobaltCoreArchipelago
{
    public class CCArchiRoute : Route
    {
        public override void Render(G g)
        {
            SharedArt.DrawEngineering(g);

            Draw.Text("Archipelago", 240, 24, font: DB.stapler, color: Colors.textMain, align: daisyowl.text.TAlign.Center);
            // TODO add more stuff
        }
    }
}
