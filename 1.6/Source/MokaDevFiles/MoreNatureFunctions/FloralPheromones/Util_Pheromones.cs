using Verse;

namespace MokaDevSpace
{
    internal class Util_Pheromones
    {
        public static bool OnePawnHasGene(Pawn pawn, Pawn other)
        {
            if ((pawn.HasRealGene(MCM_DefOf.Moka_FloralPheromones)) && !(other.HasRealGene(MCM_DefOf.Moka_FloralPheromones)))
            {
                return true;
            }
            if (!(pawn.HasRealGene(MCM_DefOf.Moka_FloralPheromones)) && (other.HasRealGene(MCM_DefOf.Moka_FloralPheromones)))
            {
                return true;
            }
            return false;
        }
    }
}
