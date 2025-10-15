using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;

namespace MFM
{
    internal class Gene_SteadyLactation : Gene
    {
        public override void PostAdd()
        {
            base.PostAdd();
            CompMilkableGene comp = this.pawn.GetComp<CompMilkableGene>();
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
            CompMilkableGene comp = this.pawn.GetComp<CompMilkableGene>();
            if (comp != null)
                comp.geneIsPresent = false;
            base.PostRemove();
        }
    }
}
