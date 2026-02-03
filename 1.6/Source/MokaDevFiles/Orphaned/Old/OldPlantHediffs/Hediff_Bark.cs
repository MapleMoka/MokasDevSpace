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
    public class Hediff_BarkOld : HediffWithComps
    {
        private HediffComp_BarkOld cachedComp;
        private float cachedAge = -1f;
        private HediffStage cachedStage;

        private HediffComp_BarkOld Comp
        {
            get => this.cachedComp ?? (this.cachedComp = this.TryGetComp<HediffComp_BarkOld>());
        }

        public override HediffStage CurStage
        {
            get
            {
                if (Mathf.Approximately(this.cachedAge, this.Comp.barkLayers))
                    return this.cachedStage;
                this.cachedAge = this.Comp.barkLayers;
                this.cachedStage = new HediffStage()
                {
                    capMods = new List<PawnCapacityModifier>(1)
          {
            new PawnCapacityModifier()
            {
              capacity = PawnCapacityDefOf.Moving,
              offset = this.Comp.Props.slowdownCurve.Evaluate(this.cachedAge)
            }
          },
                    statOffsets = new List<StatModifier>(2)
          {
            new StatModifier()
            {
              stat = StatDefOf.ArmorRating_Blunt,
              value = this.Comp.Props.bluntProtectionCurve.Evaluate(this.cachedAge)
            },
            new StatModifier()
            {
              stat = StatDefOf.MeatAmount,
              value = this.Comp.Props.staggerStunCurve.Evaluate(this.cachedAge)
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
