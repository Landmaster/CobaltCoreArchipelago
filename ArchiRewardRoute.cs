namespace CobaltCoreArchipelago
{
    class ArchiRewardRoute : Route, OnMouseDown {
        public long locationId = -1;

        public void OnMouseDown(G g, Box b)
        {
            CCArchiData.Session!.Locations.CompleteLocationChecks(locationId);
            g.CloseRoute(this);
        }

        public override void Render(G g)
        {
            SharedArt.DrawEngineering(g);

            Draw.Text("Archipelago Reward", 240, 44, font: DB.stapler, color: Colors.textMain, align: daisyowl.text.TAlign.Center);
            Draw.Text(
                CCArchiData.Instance.LocationToItem[locationId] ?? locationId.ToString(),
                240, 70, color: Colors.textMain.gain(0.5), align: daisyowl.text.TAlign.Center
            );
            var rect = new Rect(240 - 128.0 / 2, 80, 128, 128);
            g.Push(key: ArchiUKs.ArchiRewardButton, rect: rect, onMouseDown: this);
            Draw.Sprite((Spr?)CCArchiSprites.ArchiIconLarge!.Id, rect.x, rect.y);
            g.Pop();
        }
    }
}
