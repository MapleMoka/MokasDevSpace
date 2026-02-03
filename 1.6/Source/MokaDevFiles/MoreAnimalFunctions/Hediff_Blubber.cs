using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace MokaDevSpace
{
    public class Hediff_Blubber : HediffWithComps
    {
        private HediffComp_Blubber cachedComp;
        private float cachedBlubber = -1f;
        private HediffStage cachedStage;

        private HediffComp_Blubber Comp
        {
            get => this.cachedComp ?? (this.cachedComp = this.TryGetComp<HediffComp_Blubber>());
        }

        public override HediffStage CurStage
        {
            get
            {
                if (Mathf.Approximately(this.cachedBlubber, this.Comp.blubberReserves))
                    return this.cachedStage;
                this.cachedBlubber = this.Comp.blubberReserves;
                this.cachedStage = new HediffStage()
                {
                    capMods = new List<PawnCapacityModifier>(1)
                    {
                        new PawnCapacityModifier()
                        {
                            capacity = PawnCapacityDefOf.Moving,
                            offset = this.Comp.Props.speedUpCurve.Evaluate(this.cachedBlubber)
                        }
                    },
                    statOffsets = new List<StatModifier>(2)
                    {
                        new StatModifier()
                        {
                            stat = StatDefOf.RestFallRateFactor,
                            value = this.Comp.Props.tirednessReductionCurve.Evaluate(this.cachedBlubber)
                        },
                        new StatModifier()
                        {
                            stat = StatDefOf.GlobalLearningFactor,
                            value = this.Comp.Props.learningRateCurve.Evaluate(this.cachedBlubber)
                        }
                    }
                };
                return this.cachedStage;
            }
        }

        public override void Notify_PawnDied(DamageInfo? dinfo, Hediff culprit = null)
        {
            base.Notify_PawnDied(dinfo, culprit);
            this.Comp.cachedNeed = (Need_Food)null;
        }

    }
}
