using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_LumenToPsych : HediffComp
    {
        private Gene_Resource_Lumens cachedToxonGene;

        public HediffCompProperties_LumenToPsych Props => (HediffCompProperties_LumenToPsych)props;

        public override bool CompShouldRemove => base.Pawn.genes?.GetFirstGeneOfType<Gene_Resource_Lumens>() == null;

        private Gene_Resource_Lumens Lumen => cachedToxonGene ?? (cachedToxonGene = base.Pawn.genes.GetFirstGeneOfType<Gene_Resource_Lumens>());

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (Lumen.Value >= 0.75)
            {
                this.parent.Severity = 3;
            }
            else if (Lumen.Value <= 0.25)
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
