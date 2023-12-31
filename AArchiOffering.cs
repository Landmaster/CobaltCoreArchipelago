namespace CobaltCoreArchipelago
{
    class AArchiOffering : CardAction
    {
        public override Route? BeginWithRoute(G g, State s, Combat c)
        {
            return new ArchiRewardRoute();
        }

        class ArchiRewardRoute : Route {
            public override void Render(G g)
            {
                SharedArt.DrawEngineering(g);
                
                Draw.Text("Archipelago Reward", 240, 44, font: DB.stapler, color: Colors.textMain, align: daisyowl.text.TAlign.Center);
                Draw.Sprite((Spr?)CCArchiSprites.ArchiIconLarge!.Id, 240 - 128/2, 80);
                // TODO add more stuff
            }
        }
    }
}
