using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;

namespace MokaDevSpace
{
    internal class Gene_Toxosynthesis : Gene
    {
        private float rsrcLvlRequired;
        private float foodLvlMax;
        private float foodFillRate;

        private Gene_Resource_Pollution cachedLightGene;
        private Need_Food cachedFoodNeed;
        private Ext_ResourceToFoodGene ext;

        private Gene_Resource_Pollution lightGene => cachedLightGene ?? (cachedLightGene = base.pawn.genes.GetFirstGeneOfType<Gene_Resource_Pollution>());
        private Need_Food foodNeed => cachedFoodNeed ?? (cachedFoodNeed = base.pawn.needs.TryGetNeed<Need_Food>());

        public override void PostAdd()
        {
            base.PostAdd();
            ext = this.def.GetModExtension<Ext_ResourceToFoodGene>();
            
            this.foodLvlMax = ext.foodLvlMax;
            this.rsrcLvlRequired = ext.rsrcLvlRequired;
            this.foodFillRate = ext.foodFillRate;
            
        }

        public override void Tick()
        {
            base.Tick();
            if (!this.pawn.IsColonist && !this.pawn.IsPrisonerOfColony || this.pawn.Map == null)
                return;
            if (this.lightGene.Value > this.rsrcLvlRequired)
                this.AdjustFood();
        }
        public void AdjustFood()
        {
            if (this.foodNeed.CurLevel <= this.foodLvlMax)
            {
                this.foodNeed.CurLevel += this.foodFillRate;
            }
        }

        public override void PostMake() => base.PostMake();

        public override void PostRemove() => base.PostRemove();

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<float>(ref this.rsrcLvlRequired, "lightLevelRequired", this.ext.rsrcLvlRequired);
            Scribe_Values.Look<float>(ref this.foodLvlMax, "maxFoodLevel", this.ext.foodLvlMax);
            Scribe_Values.Look<float>(ref this.foodFillRate, "foodFillRate", this.ext.foodFillRate);
        }

        public Gene_Toxosynthesis()
        {
            this.rsrcLvlRequired = 0.5f;
            this.foodFillRate = 0.0001f;
            this.foodLvlMax = 0.2f;
        }
    }
}