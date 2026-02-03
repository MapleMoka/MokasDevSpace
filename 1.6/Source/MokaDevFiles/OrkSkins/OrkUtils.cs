using Verse;

namespace MokaDevSpace
{
    public static class OrkUtils
    {
        public static bool IsOrkSkin(this Pawn pawn)
        {
            if (!ModsConfig.BiotechActive || pawn.genes == null)
            {
                return false;
            }
            return pawn.genes.HasActiveGene(MCM_DefOf.Moka_FungusTouched);
        }

        public static void OffsetFungus(Pawn pawn, float offset, bool applyStatFactor = true)
        {
            if (!ModsConfig.BiotechActive)
            {
                return;
            }
            //if (offset > 0f && applyStatFactor)
            //{
            //    offset *= pawn.GetStatValue(MCM_DefOf.Moka_PollutionGainFactor);
            //}
            //Gene_Resource_NaniteDrain gene_NaniteDrain = pawn.genes?.GetFirstGeneOfType<Gene_Resource_NaniteDrain>();
            //if (gene_NaniteDrain != null)
            //{
            //    GeneResourceDrainUtility.OffsetResource(gene_NaniteDrain, offset);
            //    return;
            //}
            Gene_Resource_Fungus gene_Fungus = pawn.genes?.GetFirstGeneOfType<Gene_Resource_Fungus>();
            if (gene_Fungus != null)
            {
                gene_Fungus.Value += offset;
            }
        } 
    }
}
