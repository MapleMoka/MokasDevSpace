using RimWorld;
using Verse;

namespace MokaDevSpace
{
    internal class Thoughtworker_LocAffinityStrong : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn otherPawn)
        {
            if (!Util_LocAffinity.SharesLocAffinityStrongGene(p, otherPawn) || !RelationsUtility.PawnsKnowEachOther(p, otherPawn))
            {
                return false;
            }
            if (otherPawn.HasRealGene(MCM_DefOf.Moka_LocAffinityStrong))
            {
                return ThoughtState.ActiveAtStage(1);
            }
            return false;
        }
    }
}