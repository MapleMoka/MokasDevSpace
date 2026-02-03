using RimWorld;
using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_HemoToPsych : HediffComp
    {
        private Gene_Hemogen cachedToxonGene;

        public HediffCompProperties_HemoToPsych Props => (HediffCompProperties_HemoToPsych)props;

        public override bool CompShouldRemove => base.Pawn.genes?.GetFirstGeneOfType<Gene_Hemogen>() == null;

        private Gene_Hemogen Hemogen => cachedToxonGene ?? (cachedToxonGene = base.Pawn.genes.GetFirstGeneOfType<Gene_Hemogen>());

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (Hemogen.Value >= 0.75)
            {
                this.parent.Severity = 3;
            }
            else if (Hemogen.Value <= 0.25)
            {
                this.parent.Severity = 1;
            }
            else
            {
                this.parent.Severity = 2;
            }
        }
    }
}
