using RimWorld;
using Verse;

namespace MokaDevSpace
{
    public static class Util_MechMethods
    {
        public static void OffsetNanites(Pawn pawn, float offset, bool applyStatFactor = true)
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
            Gene_Resource_Nanites gene_Nanite = pawn.genes?.GetFirstGeneOfType<Gene_Resource_Nanites>();
            if (gene_Nanite != null)
            {
                gene_Nanite.Value += offset;
            }
        }
    }
}
