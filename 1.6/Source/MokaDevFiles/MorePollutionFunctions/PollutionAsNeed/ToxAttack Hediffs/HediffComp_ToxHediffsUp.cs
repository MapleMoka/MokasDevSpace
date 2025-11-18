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
    internal class HediffComp_ToxHediffsUp : HediffComp
    {
        public float starchReserves;
        public Need_Pollution pollutedNeed;

        public HediffCompProperties_ToxHediffsUp Props => this.props as HediffCompProperties_ToxHediffsUp;

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
            if (this.initNeed.CurLevel >= this.initNeed.PollThreshholdForSkills)
            {
                this.parent.Severity += 0.999f;
            }
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look<float>(ref this.starchReserves, "storedFat");
        }
    }
}
