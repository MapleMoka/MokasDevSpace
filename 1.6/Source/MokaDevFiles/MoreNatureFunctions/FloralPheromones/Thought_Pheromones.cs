using RimWorld;

namespace MokaDevSpace
{
    internal class Thought_Pheromones : Thought_SituationalSocial
    {
        public override float OpinionOffset()
        {
            if (Util_Pheromones.OnePawnHasGene(pawn, OtherPawn()))
            {
                if (OtherPawn().HasRealGene(MCM_DefOf.Moka_FloralPheromones))
                {
                    return (15);
                }
                else
                {
                    return (0);
                }
            }
            else
            {
                return 0;
            }
        }
    }
}
