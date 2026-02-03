using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_AdjustByFungusLevel : HediffComp
    {
        public HediffCompProperties_AdjustByFungusLevel Props
        {
            get
            {
                return (HediffCompProperties_AdjustByFungusLevel)this.props;
            }
        }

        private Gene_Resource_Fungus cachedFungusGene;

        private Gene_Resource_Fungus localFungusGene => cachedFungusGene ?? (cachedFungusGene = base.Pawn.genes.GetFirstGeneOfType<Gene_Resource_Fungus>());

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);
            if (localFungusGene.Value >= 0.75)
            {
                this.parent.Severity = 3;
            }
            else if (localFungusGene.Value >= 0.5)
            {
                this.parent.Severity = 2;
            }
            else if (localFungusGene.Value >= 0.25)
            {
                this.parent.Severity = 1;
            }
            else
            {
                this.parent.Severity = 0.0001f;
            }
        }


    }
}
