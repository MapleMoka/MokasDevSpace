using Verse;

namespace MFM
{
    public class Gene_HarvestFrom : Gene
    {
        public override void PostAdd()
        {
            base.PostAdd();
            Comp_HarvestFromGene comp = ((ThingWithComps)this.pawn).GetComp<Comp_HarvestFromGene>();
            if (comp == null)
                return;
            comp.geneIsPresent = true;
            comp.pawnProduce = ((Def)this.def).GetModExtension<Ext_HarvestFromGene>().pawnProduce;
            comp.amount = ((Def)this.def).GetModExtension<Ext_HarvestFromGene>().amount;
            comp.daysToProduce = ((Def)this.def).GetModExtension<Ext_HarvestFromGene>().daysToProduce;
        }

        public override void PostMake() => base.PostMake();

        public override void PostRemove()
        {
            Comp_HarvestFromGene comp = ((ThingWithComps)this.pawn).GetComp<Comp_HarvestFromGene>();
            if (comp != null)
                comp.geneIsPresent = false;
            base.PostRemove();
        }
    }
}
