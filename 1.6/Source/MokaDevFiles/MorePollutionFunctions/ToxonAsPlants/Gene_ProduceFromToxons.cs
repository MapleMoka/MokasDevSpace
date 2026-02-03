using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;

namespace MokaDevSpace
{
    internal class Gene_ProduceFromToxons : Gene
    {
        private ThingDef produce;
        private int amount;
        private int interval;
        private int intervalLeft;
        private float lightCost;

        private Gene_Resource_Toxon cachedLightGene;

        private Gene_Resource_Toxon lightGene => cachedLightGene ?? (cachedLightGene = base.pawn.genes.GetFirstGeneOfType<Gene_Resource_Toxon>());

        public override void PostAdd()
        {
            base.PostAdd();
            this.produce = this.def.GetModExtension<Ext_ProduceFromGeneGiz>().produces;
            this.amount = this.def.GetModExtension<Ext_ProduceFromGeneGiz>().amount;
            this.interval = this.def.GetModExtension<Ext_ProduceFromGeneGiz>().intervalDays * (60000/4);
            this.intervalLeft = this.def.GetModExtension<Ext_ProduceFromGeneGiz>().intervalDays * (60000 / 4);
            this.lightCost = this.def.GetModExtension<Ext_ProduceFromGeneGiz>().lightCost;
            
        }

        public override void Tick()
        {
            base.Tick();
            if (!this.pawn.IsColonist && !this.pawn.IsPrisonerOfColony || this.pawn.Map == null || !this.pawn.IsHashIntervalTick(this.interval))
                return;
            this.CreateProduce();
            this.lightGene.Value -= this.lightCost;
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
            Scribe_Values.Look<int>(ref this.intervalLeft, "coatIntervalLeft", this.def.GetModExtension<Ext_ProduceFromGeneGiz>().intervalDays * 60000, false);
            Scribe_Defs.Look<ThingDef>(ref this.produce, "coatProduce");
            Scribe_Values.Look<int>(ref this.interval, "coatInterval", this.def.GetModExtension<Ext_ProduceFromGeneGiz>().intervalDays * 60000, false);
            Scribe_Values.Look<int>(ref this.amount, "coatamount", this.def.GetModExtension<Ext_ProduceFromGeneGiz>().amount, false);
            this.produce = this.def.GetModExtension<Ext_ProduceFromGeneGiz>().produces;
        }

        public Gene_ProduceFromToxons()
        {
            this.amount = 0;
            this.interval = 0;
            this.intervalLeft = 0;
        }
    }
}