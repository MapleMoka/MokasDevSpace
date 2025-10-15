using Verse;

namespace MFM
{
    public class Gene_ThickCoat : Gene
    {
        public override void PostAdd()
        {
            base.PostAdd();
            CompShearableGene comp = ((ThingWithComps)this.pawn).GetComp<CompShearableGene>();
            if (comp == null)
                return;
            comp.geneIsPresent = true;
            comp.pawnProduce = ((Def)this.def).GetModExtension<GeneTriggeredProduce>().pawnProduce;
            comp.amount = ((Def)this.def).GetModExtension<GeneTriggeredProduce>().amount;
            comp.daysToProduce = ((Def)this.def).GetModExtension<GeneTriggeredProduce>().daysToProduce;
        }

        public override void PostMake() => base.PostMake();

        public override void PostRemove()
        {
            CompShearableGene comp = ((ThingWithComps)this.pawn).GetComp<CompShearableGene>();
            if (comp != null)
                comp.geneIsPresent = false;
            base.PostRemove();
        }
    }
}
