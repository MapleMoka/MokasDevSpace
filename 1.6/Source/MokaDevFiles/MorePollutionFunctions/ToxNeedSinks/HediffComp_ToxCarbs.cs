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
    internal class HediffComp_ToxCarbs : HediffComp
    {
        public float starchReserves;
        public Need_Food nutrientNeed;
        public Need_Pollution toxicNeed;

        public HediffCompProperties_ToxCarbs Props => this.props as HediffCompProperties_ToxCarbs;

        public override string CompTipStringExtra
        {
            get
            {
                return (string)"MokaDevSpace.StarchReserves".Translate((NamedArgument)(Math.Round((double)this.starchReserves, 2)*100));
            }
        }

        public Need_Food needToFill
        {
            get => this.nutrientNeed ?? (this.nutrientNeed = this.parent.pawn.needs.TryGetNeed<Need_Food>());
        }

        public Need_Pollution needToWeigh
        {
            get => this.toxicNeed ?? (this.toxicNeed = this.parent.pawn.needs.TryGetNeed<Need_Pollution>());
        }

        public override void CompPostTickInterval(ref float severityAdjustment, int delta)
        {
            base.CompPostTickInterval(ref severityAdjustment, delta);
            this.ApplyFatChanges(delta);
        }

        public void ApplyFatChanges(int multiplier)
        {
            if (this.needToFill == null || this.needToWeigh == null)
                return;
            if ((double)this.needToWeigh.CurLevel > (double)this.Props.startThreshold && (double)this.starchReserves < (double)this.Props.maxStarch /*&& (Util_PollutionUtils.OnPollutedTile(this.Pawn) || (double) this.needToWeigh.CurLevel >= this.toxicNeed.PollThreshholdForSkills)*/)
            {
                //this.starchReserves += Math.Min(this.Props.conversionSpeed, this.Props.maxStarch - this.starchReserves) * (float)multiplier;
                //this.needToWeigh.CurLevel -= Math.Min(this.needToWeigh.CurLevel, this.Props.conversionSpeed) * (float)multiplier;
                this.needToWeigh.CurLevel -= Math.Min(this.Props.conversionSpeed, this.Props.maxStarch - this.starchReserves) * (float)multiplier * this.Props.conversionRate;
                this.starchReserves += Math.Min(this.Props.conversionSpeed, this.Props.maxStarch - this.starchReserves) * (float)multiplier;
            }
            else
            {
                if ((double)this.needToFill.CurLevel >= (double)this.Props.starvationThreshold || (double)this.starchReserves <= 0.0)
                    return;
                this.needToFill.CurLevel += Math.Min(this.starchReserves, this.Props.starvationSpeed) * (float)multiplier;
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
