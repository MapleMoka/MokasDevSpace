using RimWorld;
using System;
using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_Blubber : HediffComp
    {
        public float blubberReserves;
        public Need_Food cachedNeed;

        public HediffCompProperties_Blubber Props => this.props as HediffCompProperties_Blubber;

        public override string CompTipStringExtra
        {
            get
            {
                return (string)"MokaDevSpace.BlubberReserves".Translate((NamedArgument)(Math.Round((double)this.blubberReserves, 2)*100));
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
            if ((double)this.blubberReserves < (double)this.Props.maxBlubber && this.localNeed.CurLevel <= this.Props.startThreshold)
            {
                this.localNeed.CurLevel -= Math.Min(this.blubberReserves, this.Props.conversionSpeed) * (float)multiplier;
                this.blubberReserves += Math.Min(this.Props.conversionSpeed, this.Props.maxBlubber - this.blubberReserves) * (float)multiplier;
            }
            else
            {
                if ((double)this.localNeed.CurLevel >= (double)this.Props.starvationThreshold || (double)this.blubberReserves <= 0.0)
                    return;
                this.localNeed.CurLevel += Math.Min(this.blubberReserves, this.Props.starvationSpeed) * (float)multiplier;
                this.blubberReserves -= Math.Min(this.blubberReserves, this.Props.starvationSpeed) * (float)multiplier;
            }
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look<float>(ref this.blubberReserves, "storedFat");
        }
    }
}
