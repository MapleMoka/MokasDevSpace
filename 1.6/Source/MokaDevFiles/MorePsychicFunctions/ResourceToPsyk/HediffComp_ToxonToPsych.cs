using RimWorld;
using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_ToxonToPsych : HediffComp
    {
        private Gene_Resource_Toxon cachedToxonGene;

        public HediffCompProperties_ToxonToPsych Props => (HediffCompProperties_ToxonToPsych)props;

        public override bool CompShouldRemove => base.Pawn.genes?.GetFirstGeneOfType<Gene_Resource_Toxon>() == null;

        private Gene_Resource_Toxon Toxon => cachedToxonGene ?? (cachedToxonGene = base.Pawn.genes.GetFirstGeneOfType<Gene_Resource_Toxon>());

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (Toxon.Value >= 0.75)
            {
                this.parent.Severity = 3;
            }
            else if (Toxon.Value <= 0.25)
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
