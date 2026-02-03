using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_SeverityFromFungus : HediffComp
    {
        private Gene_Resource_Fungus cachedFungusGene;

        public HediffCompProperties_SeverityFromFungus Props => (HediffCompProperties_SeverityFromFungus)props;

        public override bool CompShouldRemove => base.Pawn.genes?.GetFirstGeneOfType<Gene_Resource_Fungus>() == null;

        private Gene_Resource_Fungus Fungus => cachedFungusGene ?? (cachedFungusGene = base.Pawn.genes.GetFirstGeneOfType<Gene_Resource_Fungus>());

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (Fungus != null)
            {
                severityAdjustment += ((Fungus.Value > 0f) ? Props.severityPerHourLoaded : Props.severityPerHourEmpty) / 2500f;
            }
        }
    }
}
