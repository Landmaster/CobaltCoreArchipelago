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

        public const int NumCardRewards = 10;

        public const int NumArtifacts = 7;

        [JsonProperty]
        public readonly HashSet<UK> RedeemedItemIds = new();

        [JsonProperty]
        public int CardDrawCount { get; set; } = 0;

        [JsonProperty]
        public int RareCardDrawCount { get; set; } = 0;

        [JsonProperty]
        public int ArtifactCount { get; set; } = 0;

        [JsonProperty]
        public int BossArtifactCount { get; set; } = 0;

        [JsonProperty]
        public readonly List<List<Card>> ArchiCardChoices = new();

        [JsonProperty]
        public readonly List<List<Card>> ArchiRareCardChoices = new();

        [JsonProperty]
        public readonly Dictionary<long, string> LocationToItem = new();

        [JsonProperty]
        public readonly Rand ArchiArtifactOfferingRand = new();
    }
}
