using Archipelago.MultiClient.Net;

namespace CobaltCoreArchipelago
{
    public class CCArchiData
    {
        public static ArchipelagoSession? Session {  get; set; }

        public static readonly HashSet<long> UnredeemedItemIds = new();
    }
}
