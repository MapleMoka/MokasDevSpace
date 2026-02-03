using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_SeverityFromDust : HediffComp
    {
        private Gene_Resource_Dust cachedDustGene;

        public HediffCompProperties_SeverityFromDust Props => (HediffCompProperties_SeverityFromDust)props;

        public override bool CompShouldRemove => base.Pawn.genes?.GetFirstGeneOfType<Gene_Resource_Dust>() == null;

        private Gene_Resource_Dust Dust => cachedDustGene ?? (cachedDustGene = base.Pawn.genes.GetFirstGeneOfType<Gene_Resource_Dust>());

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (Dust != null)
            {
                severityAdjustment += ((Dust.Value > 0f) ? Props.severityPerHourLoaded : Props.severityPerHourEmpty) / 2500f;
            }
        }
    }
}
