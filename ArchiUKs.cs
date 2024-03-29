﻿namespace CobaltCoreArchipelago
{
    public class ArchiUKs
    {
        public const UK ArchiButton = (UK)49201;
        public const UK ArchiRewardButton = (UK)49202;

        public static UK CardRewardUK(int index) { 
            return (UK)(70600 + index);
        }

        public static UK ArtifactUK(int index) { 
            return (UK)(70700 + index);
        }

        public const UK RareCardReward1UK = (UK)70800;
        public const UK RareCardReward2UK = (UK)70801;
        public const UK BossArtifact1UK = (UK)70900;
        public const UK BossArtifact2UK = (UK)70901;

        public static long UKToArchiLocation(UK uk) {
            int ukAsInt = (int)uk;
            // Card Reward
            if (70600 <= ukAsInt && ukAsInt < 70700)
            {
                return 185001 + (ukAsInt - 70600);
            }
            // Artifact Reward
            else if (70700 <= ukAsInt && ukAsInt < 70800)
            {
                return 187001 + (ukAsInt - 70700);
            }
            else {
                return ukAsInt switch
                {
                    (int)RareCardReward1UK => 186001,
                    (int)RareCardReward2UK => 186002,
                    (int)BossArtifact1UK => 188001,
                    (int)BossArtifact2UK => 188002,
                    _ => -1
                }; 
            }
        }

        public static IEnumerable<UK> AllUKs() {
            return Enumerable.Range(0, CCArchiData.NumCardRewards).Select(CardRewardUK)
                .Concat(Enumerable.Range(0, CCArchiData.NumArtifacts).Select(ArtifactUK))
                .Concat(new UK[] { RareCardReward1UK, RareCardReward2UK, BossArtifact1UK, BossArtifact2UK });
        }
    }
}
