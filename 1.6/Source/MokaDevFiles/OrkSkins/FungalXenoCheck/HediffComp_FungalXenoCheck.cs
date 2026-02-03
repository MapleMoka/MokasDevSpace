using System.Linq;
using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_FungalXenoCheck : HediffComp
    {
        public HediffCompProperties_FungalXenoCheck Props
        {
            get
            {
                return (HediffCompProperties_FungalXenoCheck)this.props;
            }
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);
            if (this.parent.Severity >= this.Props.changeAtSeverity)
            {
                foreach (Gene gene in this.parent.pawn.genes.GenesListForReading.ToList<Gene>())
                {
                    this.parent.pawn.genes.RemoveGene(gene);
                }

                this.parent.pawn.genes.SetXenotypeDirect(this.Props.xenotype);
                for (int index = 0; index < this.Props.xenotype.genes.Count; ++index)
                    this.parent.pawn.genes.AddGene(this.Props.xenotype.genes[index], !this.Props.xenotype.inheritable);
                //this.parent.pawn.genes.xenotypeName = this.Props.xenotype.defName;
                //this.parent.pawn.genes.xenotype.Icon = this.Props.xenotype.Icon;
                this.parent.Severity = 1f;
            }
        }

        public HediffComp_FungalXenoCheck()
        {
        }
    }
}
