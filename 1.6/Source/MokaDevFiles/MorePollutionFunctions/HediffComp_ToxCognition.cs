using MFM;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_ToxCognition : HediffComp
    {
        public float starchReserves;
        public Need_Food cachedNeed;

        public HediffCompProperties_ToxCognition Props => this.props as HediffCompProperties_ToxCognition;

        public override string CompTipStringExtra
        {
            get
            {
                return (string)"MokaDevSpace.StarchReserves".Translate((NamedArgument)Math.Round((double)this.starchReserves, 2));
            }
        }

        public Need_Food localNeed
        {
            get => this.cachedNeed ?? (this.cachedNeed = this.parent.pawn.needs.TryGetNeed<Need_Food>());
        }

        public override void CompPostTickInterval(ref float severityAdjustment, int delta)
        {
            base.CompPostTickInterval(ref severityAdjustment, delta);
            this.ApplyFatChanges(delta);
        }

        public void ApplyFatChanges(int multiplier)
        {
            if (this.localNeed == null)
                return;
            if ((double)this.starchReserves < (double)this.Props.maxStarch && Utils_MapBasedEffects.PawnInLight(this.Pawn))
            {
                this.starchReserves += Math.Min(this.Props.conversionSpeed, this.Props.maxStarch - this.starchReserves) * (float)multiplier;
            }
            else
            {
                if ((double)this.localNeed.CurLevel >= (double)this.Props.starvationThreshold || (double)this.starchReserves <= 0.0)
                    return;
                this.localNeed.CurLevel += Math.Min(this.starchReserves, this.Props.starvationSpeed) * (float)multiplier;
                this.starchReserves -= Math.Min(this.starchReserves, this.Props.starvationSpeed) * (float)multiplier;
            }
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look<float>(ref this.starchReserves, "storedFat");
        }
    }
}
