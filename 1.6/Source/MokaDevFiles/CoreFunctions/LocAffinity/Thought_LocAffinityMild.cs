using RimWorld;

namespace MokaDevSpace
{
    internal class Thought_LocAffinityMild : Thought_SituationalSocial
    {
        public override float OpinionOffset()
        {
            return Util_LocAffinity.SharesLocAffinityMildGene(pawn, OtherPawn()) ? (OtherPawn().HasRealGene(MCM_DefOf.Moka_LocAffinityMild) ? 10 : 0) : 0;
        }
    }
}
