using RimWorld;
using Verse;

namespace MFM
{
    internal class CompPluckableGene : CompHasGatherableBodyResource
    {
        public bool geneIsPresent;
        public ThingDef pawnProduce;
        public int amount = 45;
        public int daysToProduce = 10;

        protected override int GatherResourcesIntervalDays => this.daysToProduce;

        protected override int ResourceAmount => this.amount;

        protected override ThingDef ResourceDef => this.pawnProduce;

        protected override string SaveKey => "pluckablefruit";

        public CompProperties_PluckableGene Props => (CompProperties_PluckableGene)this.props;

        protected override bool Active => base.Active && (!(this.parent is Pawn parent) || this.geneIsPresent /*&& (double)parent.ageTracker.AgeBiologicalYears >= (double)MFM_Produce_DefOf.Moka_ResourceProductionGene.minAgeActive*/);

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look<float>(ref this.fullness, this.SaveKey);
            Scribe_Values.Look<int>(ref this.amount, "pluckableGeneAmount", 45);
            Scribe_Values.Look<int>(ref this.daysToProduce, "pluckableGeneDays", 10);
            Scribe_Values.Look<bool>(ref this.geneIsPresent, "pluckableGenePresent");
            Scribe_Defs.Look<ThingDef>(ref this.pawnProduce, "pluckableProduct");
            //if (this.pawnProduce == null)
            //    this.pawnProduce = MFM_Produce_DefOf.Steel;
            if (this.amount == 0)
                this.amount = 45;
            if (this.daysToProduce != 0)
                return;
            this.daysToProduce = 10;
        }

        public override string CompInspectStringExtra() => !this.Active ? (string)null : (string)("Moka_PluckingAmount".Translate() + ": " + this.Fullness.ToStringPercent());
    }
}
