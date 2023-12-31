using Archipelago.MultiClient.Net;
using Newtonsoft.Json;

namespace CobaltCoreArchipelago
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CCArchiData
    {
        public static CCArchiData Instance { get; set; } = new();

        public static ArchipelagoSession? Session {  get; set; }

        public static Dictionary<string, object>? SlotData { get; set; }

        public static StarterShip? StarterShipFromId(long id)
        {
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

        [JsonProperty]
        public readonly HashSet<long> RedeemedItemIds = new();

        [JsonProperty]
        public int CardDrawCount { get; set; } = 0;
    }
}
