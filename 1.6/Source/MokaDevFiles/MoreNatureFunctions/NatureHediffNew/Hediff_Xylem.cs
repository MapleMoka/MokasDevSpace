using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace MokaDevSpace
{
    public class Hediff_Xylem : HediffWithComps
    {
        private HediffComp_Xylem cachedComp;
        private float cachedWater = -1f;
        private HediffStage cachedStage;

        private HediffComp_Xylem Comp
        {
            get => this.cachedComp ?? (this.cachedComp = this.TryGetComp<HediffComp_Xylem>());
        }

        public override HediffStage CurStage
        {
            get
            {
                if (Mathf.Approximately(this.cachedWater, this.Comp.waterStorage))
                    return this.cachedStage;
                this.cachedWater = this.Comp.waterStorage;
                this.cachedStage = new HediffStage()
                {
                    statOffsets = new List<StatModifier>(2)
                    {
                        new StatModifier()
                        {
                            stat = StatDefOf.Flammability,
                            value = this.Comp.Props.flameDMGCurve.Evaluate(this.cachedWater)
                        },
                        new StatModifier()
                        {
                                stat = StatDefOf.Fertility,
                                value = this.Comp.Props.fertilityCurve.Evaluate(this.cachedWater)
                        },
                        new StatModifier()
                        {
                                stat = StatDefOf.InjuryHealingFactor,
                                value = this.Comp.Props.injuryHealCurve.Evaluate(this.cachedWater)
                        },
                        new StatModifier()
                        {
                                stat = StatDefOf.MaxNutrition,
                                value = this.Comp.Props.nutritionCapCurve.Evaluate(this.cachedWater)
                        }
                    }
                };
                return this.cachedStage;
            }
        }

        public override void Notify_PawnDied(DamageInfo? dinfo, Hediff culprit = null)
        {
            base.Notify_PawnDied(dinfo, culprit);
            //this.Comp.cachedNeed = (Need_Food)null;
        }

    }
}
