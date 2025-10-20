using Verse;

namespace MFM
{
    internal class Gene_FruitBearing : Gene
    {
        public override void PostAdd()
        {
            base.PostAdd();
            CompPluckableGene comp = this.pawn.GetComp<CompPluckableGene>();
            if (comp == null)
                return;
            comp.geneIsPresent = true;
            comp.pawnProduce = this.def.GetModExtension<GeneTriggeredProduce>().pawnProduce;
            comp.amount = this.def.GetModExtension<GeneTriggeredProduce>().amount;
            comp.daysToProduce = this.def.GetModExtension<GeneTriggeredProduce>().daysToProduce;
        }

        public override void PostMake() => base.PostMake();

        public override void PostRemove()
        {
            CompPluckableGene comp = this.pawn.GetComp<CompPluckableGene>();
            if (comp != null)
                comp.geneIsPresent = false;
            base.PostRemove();
        }
    }
}
