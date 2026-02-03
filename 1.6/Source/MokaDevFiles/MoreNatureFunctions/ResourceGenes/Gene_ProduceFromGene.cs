using Verse;

namespace MokaDevSpace
{
    internal class Gene_ProduceFromGene : Gene
    {
        private ThingDef produce;
        private int amount;
        private int interval;
        private int intervalLeft;

        public override void PostAdd()
        {
            base.PostAdd();
            this.produce = this.def.GetModExtension<Ext_ProduceFromGene>().produces;
            this.amount = this.def.GetModExtension<Ext_ProduceFromGene>().amount;
            this.interval = this.def.GetModExtension<Ext_ProduceFromGene>().intervalDays * (60000/4);
            this.intervalLeft = this.def.GetModExtension<Ext_ProduceFromGene>().intervalDays * (60000 / 4);
            
        }

        public override void Tick()
        {
            base.Tick();
            if (!this.pawn.IsColonist && !this.pawn.IsPrisonerOfColony || this.pawn.Map == null || !this.pawn.IsHashIntervalTick(this.interval))
                return;
            this.CreateProduce();
        }

        public Thing CreateProduce()
        {
            Thing produce = GenSpawn.Spawn(this.produce, this.pawn.Position, this.pawn.Map, WipeMode.VanishOrMoveAside);
            produce.stackCount = this.amount;
            return produce;
        }

        public override void PostMake() => base.PostMake();

        public override void PostRemove() => base.PostRemove();

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.intervalLeft, "coatIntervalLeft", this.def.GetModExtension<Ext_ProduceFromGene>().intervalDays * 60000, false);
            Scribe_Defs.Look<ThingDef>(ref this.produce, "coatProduce");
            Scribe_Values.Look<int>(ref this.interval, "coatInterval", this.def.GetModExtension<Ext_ProduceFromGene>().intervalDays * 60000, false);
            Scribe_Values.Look<int>(ref this.amount, "coatamount", this.def.GetModExtension<Ext_ProduceFromGene>().amount, false);
            this.produce = this.def.GetModExtension<Ext_ProduceFromGene>().produces;
        }

        public Gene_ProduceFromGene()
        {
            this.amount = 0;
            this.interval = 0;
            this.intervalLeft = 0;
        }
    }
}