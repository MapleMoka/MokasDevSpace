using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;

namespace MFM
{
    internal class CompMilkableGene : CompHasGatherableBodyResource
    {
        public bool geneIsPresent;
        public ThingDef pawnProduce;
        public int amount = 45;
        public int daysToProduce = 10;

        protected override int GatherResourcesIntervalDays => this.daysToProduce;

        protected override int ResourceAmount => this.amount;

        protected override ThingDef ResourceDef => this.pawnProduce;

        protected override string SaveKey => "milkableLevel";

        public CompProperties_MilkableGene Props => (CompProperties_MilkableGene)this.props;

        protected override bool Active => base.Active && (!(this.parent is Pawn parent) || this.geneIsPresent /* && (double)parent.ageTracker.AgeBiologicalYears >= (double)MFM_Produce_DefOf.Moka_ResourceProductionGene.minAgeActive */);
        
        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<float>(ref this.fullness, this.SaveKey);
            Scribe_Values.Look<int>(ref this.amount, "milkableGeneAmount", 45);
            Scribe_Values.Look<int>(ref this.daysToProduce, "milkableGeneDays", 10);
            Scribe_Values.Look<bool>(ref this.geneIsPresent, "milkableGenePresent");
            Scribe_Defs.Look<ThingDef>(ref this.pawnProduce, "milkableProduct");
            //if (this.pawnProduce == null)
            //    this.pawnProduce = MFM_Produce_DefOf.Steel;
            if (this.amount == 0)
                this.amount = 45;
            if (this.daysToProduce != 0)
                return;
            this.daysToProduce = 10;
        }

        public override string CompInspectStringExtra() => !this.Active ? (string)null : (string)("Moka_LactateFullness".Translate() + ": " + this.Fullness.ToStringPercent());
    }
}
