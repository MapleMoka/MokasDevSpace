using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RimWorld;
using Verse;

namespace MokaDevSpace
{
    internal class Comp_ProduceFromGene : CompHasGatherableBodyResource
    {
        public bool geneIsPresent;
        public ThingDef produce;
        public int amount;
        public int interval;
        //public XenotypeDef sourceXeno;

        protected override int GatherResourcesIntervalDays => this.interval;

        protected override int ResourceAmount => this.amount;

        protected override ThingDef ResourceDef => this.produce;

        protected override string SaveKey => "resourceGrowth";

        public CompProperties_ProduceFromGene Props => (CompProperties_ProduceFromGene)this.props;

        protected override bool Active => base.Active && (!(this.parent is Pawn) || this.geneIsPresent);
        
        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<float>(ref this.fullness, this.SaveKey, 0.0f, false);
            Scribe_Values.Look<int>(ref this.amount, "milkableGeneAmount", 45, false);
            Scribe_Values.Look<int>(ref this.interval, "milkableGeneDays", 10, false);
            Scribe_Values.Look<bool>(ref this.geneIsPresent, "milkableGenePresent", false, false);
            Scribe_Defs.Look<ThingDef>(ref this.produce, "milkableProduct");
            if (this.produce == null)
                this.produce = ThingDefOf.WoodLog;
            if (this.amount == 0)
                this.amount = 45;
            if (this.interval != 0)
                return;
            this.interval = 10;
        }

        public override string CompInspectStringExtra() => !this.Active ? (string)null : (string)("Moka_LactateFullness".Translate() + ": " + this.Fullness.ToStringPercent());

        public Comp_ProduceFromGene()
        {
            this.produce = ThingDefOf.WoodLog;
            this.amount = 45;
            this.interval = 10;
        }
    }
}
