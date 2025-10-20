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
    public class Hediff_ToxCarbs : HediffWithComps
    {
        private HediffComp_ToxCarbs cachedComp;
        private float cachedStarch = -1f;
        private HediffStage cachedStage;

        private HediffComp_ToxCarbs Comp
        {
            get => this.cachedComp ?? (this.cachedComp = this.TryGetComp<HediffComp_ToxCarbs>());
        }

        public override HediffStage CurStage
        {
            get
            {
                if (Mathf.Approximately(this.cachedStarch, this.Comp.starchReserves))
                    return this.cachedStage;
                this.cachedStarch = this.Comp.starchReserves;
                this.cachedStage = new HediffStage()
                {
                    capMods = new List<PawnCapacityModifier>(1)
                    {
                        new PawnCapacityModifier()
                        {
                            capacity = PawnCapacityDefOf.Moving,
                            offset = this.Comp.Props.speedUpCurve.Evaluate(this.cachedStarch)
                        }
                    },
                    statOffsets = new List<StatModifier>(2)
                    {
                        new StatModifier()
                        {
                            stat = StatDefOf.RestFallRateFactor,
                            value = this.Comp.Props.tirednessReductionCurve.Evaluate(this.cachedStarch)
                        },
                        new StatModifier()
                        {
                            stat = StatDefOf.GlobalLearningFactor,
                            value = this.Comp.Props.learningRateCurve.Evaluate(this.cachedStarch)
                        }
                    }
                };
                return this.cachedStage;
            }
        }

        public override void Notify_PawnDied(DamageInfo? dinfo, Hediff culprit = null)
        {
            base.Notify_PawnDied(dinfo, culprit);
            this.Comp.nutrientNeed = (Need_Food)null;
            this.Comp.toxicNeed = (Need_Pollution)null;
        }

    }
}
