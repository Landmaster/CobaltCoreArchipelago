using Archipelago.MultiClient.Net;

namespace CobaltCoreArchipelago
{
    public class CCArchiData
    {
        public static ArchipelagoSession? Session {  get; set; }

        public static readonly HashSet<long> RedeemedItemIds = new();

        public static Dictionary<string, object>? SlotData { get; set; }

        public static StarterShip? StarterShipFromId(long id) {
            return id switch
            {
                0 => StarterShip.ships["artemis"],
                1 => StarterShip.ships["ares"],
                2 => StarterShip.ships["jupiter"],
                3 => StarterShip.ships["gemini"],
                4 => StarterShip.ships["tiderunner"],
                _ => null,
            };
        }
    }
}
