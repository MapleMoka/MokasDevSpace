using RimWorld;

namespace MokaDevSpace
{
    internal class Thought_LocAffinityStrong : Thought_SituationalSocial
    {
        public override float OpinionOffset()
        {
            return Util_LocAffinity.SharesLocAffinityStrongGene(pawn, OtherPawn()) ? (OtherPawn().HasRealGene(MCM_DefOf.Moka_LocAffinityStrong) ? 20 : 0) : 0;
        }
    }
}
