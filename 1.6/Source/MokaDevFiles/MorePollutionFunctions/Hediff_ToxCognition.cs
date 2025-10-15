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
    public class Hediff_ToxCognition : HediffWithComps
    {
        private HediffComp_ToxCognition cachedComp;
        private float cachedStarch = -1f;
        private HediffStage cachedStage;

        private HediffComp_ToxCognition Comp
        {
            get => this.cachedComp ?? (this.cachedComp = this.TryGetComp<HediffComp_ToxCognition>());
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
              offset = this.Comp.Props.slowdownCurve.Evaluate(this.cachedStarch)
            }
          },
                    statOffsets = new List<StatModifier>(2)
          {
            new StatModifier()
            {
              stat = StatDefOf.ArmorRating_Blunt,
              value = this.Comp.Props.bluntProtectionCurve.Evaluate(this.cachedStarch)
            },
            new StatModifier()
            {
              stat = StatDefOf.MeatAmount,
              value = this.Comp.Props.meatCurve.Evaluate(this.cachedStarch)
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
