using Verse;

namespace MokaDevSpace
{
    internal class Util_LocAffinity
    {
        public static bool SharesLocAffinityMildGene(Pawn pawn, Pawn other)
        {
            if (pawn.HasRealGene(MCM_DefOf.Moka_LocAffinityMild) == other.HasRealGene(MCM_DefOf.Moka_LocAffinityMild))
            {
                return true;
            }
            return false;
        }
        public static bool SharesLocAffinityStrongGene(Pawn pawn, Pawn other)
        {
            if (pawn.HasRealGene(MCM_DefOf.Moka_LocAffinityStrong) == other.HasRealGene(MCM_DefOf.Moka_LocAffinityStrong))
            {
                return true;
            }
            return false;
        }
        public static bool SharesLocAffinityMixedGene(Pawn pawn, Pawn other)
        {
            if (pawn.HasRealGene(MCM_DefOf.Moka_LocAffinityStrong) == other.HasRealGene(MCM_DefOf.Moka_LocAffinityMild))
            {
                return true;
            }
            if (pawn.HasRealGene(MCM_DefOf.Moka_LocAffinityMild) == other.HasRealGene(MCM_DefOf.Moka_LocAffinityStrong))
            {
                return true;
            }
            return false;
        }
    }
}
