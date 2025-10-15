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
    internal class HediffComp_ToxTearDown : HediffComp
    {
        public float starchReserves;
        //public Need_Food cachedNeed;
        public Need_Pollution pollutedNeed;

        public HediffCompProperties_ToxTearDown Props => this.props as HediffCompProperties_ToxTearDown;

        //public override string CompTipStringExtra
        //{
        //    get
        //    {
        //        return (string)"Boglegs.StoredNutrition".Translate((NamedArgument)Math.Round((double)this.starchReserves, 2));
        //    }
        //}

        public Need_Pollution initNeed
        {
            get => this.pollutedNeed ?? (this.pollutedNeed = this.parent.pawn.needs.TryGetNeed<Need_Pollution>());
        }

        public override void CompPostTickInterval(ref float severityAdjustment, int delta)
        {
            base.CompPostTickInterval(ref severityAdjustment, delta);
            this.ApplyFatChanges(delta);
        }

        public void ApplyFatChanges(int multiplier)
        {
            if (this.initNeed == null)
                return;
            if (!this.parent.pawn.HasToxicBuildup(HediffDefOf.ToxicBuildup))
            {
                this.parent.Severity = 1;
                //return;
            }

            Hediff hediffOfTox = this.parent.pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.ToxicBuildup);
            float toxSeverityChange = this.initNeed.PollGainFromToxBuildupPerNeedIntervalTick;
            if (this.parent.pawn.HasToxicBuildup(HediffDefOf.ToxicBuildup))
            {

                //this.initNeed.CurLevel += this.initNeed.lastDelta = +this.initNeed.PollGainInToxGasPerNeedIntervalTick;
                this.initNeed.ToxBuildupChange();
                hediffOfTox.Severity -= toxSeverityChange;
                this.parent.Severity = 2;
            }
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look<float>(ref this.starchReserves, "storedFat");
        }
    }
}
