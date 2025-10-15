using RimWorld;

namespace MokaDevSpace
{
    internal class Thought_LocAffinityMixed : Thought_SituationalSocial
    {
        public override float OpinionOffset()
        {
            return Util_LocAffinity.SharesLocAffinityMixedGene(pawn, OtherPawn()) ? (OtherPawn().HasRealGene(MCM_DefOf.Moka_LocAffinityMild) ? 15 : 15) : 0;
        }
    }
}
