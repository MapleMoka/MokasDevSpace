using Verse;

namespace MokaDevSpace
{
    public static class Util_GenMethods
    {
        public static bool HasRealGene(this Pawn pawn, GeneDef geneDef)
        {
            if (pawn?.genes == null)
                return false;
            Gene gene = pawn.genes.GetGene(geneDef);
            return gene != null && gene.Active;
        }
    }
}
