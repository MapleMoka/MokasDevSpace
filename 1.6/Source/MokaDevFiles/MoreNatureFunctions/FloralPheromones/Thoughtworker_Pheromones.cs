using RimWorld;
using Verse;

namespace MokaDevSpace
{
    internal class Thoughtworker_Pheromones : ThoughtWorker
    {
        protected override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn otherPawn)
        {
            if (!Util_Pheromones.OnePawnHasGene(p, otherPawn) || !RelationsUtility.PawnsKnowEachOther(p, otherPawn))
            {
                return false;
            }
            if (otherPawn.HasRealGene(MCM_DefOf.Moka_FloralPheromones))
            {
                return ThoughtState.ActiveAtStage(1);
            }
            if (!(otherPawn.HasRealGene(MCM_DefOf.Moka_LocAffinityStrong)))
            {
                return ThoughtState.Inactive;
            }
            return false;
        }
    }
}