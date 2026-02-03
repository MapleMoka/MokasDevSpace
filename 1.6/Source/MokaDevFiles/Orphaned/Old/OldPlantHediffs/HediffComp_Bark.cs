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
    internal class HediffComp_BarkOld : HediffComp
    {
        public int barkLayers;
        //public Need_Food cachedNeed;

        public HediffCompProperties_BarkOld Props => this.props as HediffCompProperties_BarkOld;

        public override string CompTipStringExtra
        {
            get
            {
                return (string)"MokaDevSpace.StitchedBark".Translate((NamedArgument)this.parent.pawn.NameShortColored.ToString(),this.barkLayers);
            }
        }

        public override void CompPostTickInterval(ref float severityAdjustment, int delta)
        {
            base.CompPostTickInterval(ref severityAdjustment, delta);
            this.ApplyFatChanges(delta);
        }

        public void ApplyFatChanges(int multiplier)
        {
            if (this.Pawn == null)
                return;
            this.barkLayers = this.Pawn.ageTracker.AgeBiologicalYears;
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look<int>(ref this.barkLayers, "storedFat");
        }
    }
}
