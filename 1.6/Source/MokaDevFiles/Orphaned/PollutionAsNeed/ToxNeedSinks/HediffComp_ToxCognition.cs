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
        public Need_Pollution cachedNeed;

        public HediffCompProperties_ToxCognition Props => this.props as HediffCompProperties_ToxCognition;

        //public override string CompTipStringExtra
        //{
        //    get
        //    {
        //        return (string)"MokaDevSpace.StarchReserves".Translate((NamedArgument)Math.Round((double)this.starchReserves, 2));
        //    }
        //}

        public Need_Pollution localNeed
        {
            get => this.cachedNeed ?? (this.cachedNeed = this.parent.pawn.needs.TryGetNeed<Need_Pollution>());
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

            if (this.localNeed.CurLevel <= 0.25)
                this.parent.Severity = 1;
            else if (this.localNeed.CurLevel <= 0.75)
                this.parent.Severity = 2;
            else 
                this.parent.Severity = 3;
            //this.parent.pawn.ideo.SetIdeo(this.parent.pawn.ideo.Ideo);
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look<float>(ref this.starchReserves, "storedFat");
        }
    }
}
