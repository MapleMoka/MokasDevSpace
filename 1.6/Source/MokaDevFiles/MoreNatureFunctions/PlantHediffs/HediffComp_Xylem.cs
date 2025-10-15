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
    internal class HediffComp_Xylem : HediffComp
    {
        public float waterStorage;

        public HediffCompProperties_Xylem Props => this.props as HediffCompProperties_Xylem;

        public override string CompTipStringExtra
        {
            get
            {
                return (string)"MokaDevSpace.XylemStores".Translate((NamedArgument)(Math.Round((double)this.waterStorage, 2)*100));
            }
        }

        public override void CompPostTickInterval(ref float severityAdjustment, int delta)
        {
            base.CompPostTickInterval(ref severityAdjustment, delta);
            this.ApplyFatChanges(delta);
        }

        public void ApplyFatChanges(int multiplier)
        {
            if (Utils_MapBasedEffects.PawnIsWet(this.Pawn))
            {
                this.waterStorage += Math.Min(this.Props.intakeSpeed, this.Props.maxStorage - this.waterStorage) * (float)multiplier;
            }
            else
            {
                this.waterStorage -= Math.Min(this.waterStorage, this.Props.evaporationSpeed) * (float)multiplier;
            }
        }

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look<float>(ref this.waterStorage, "storedFat");
        }
    }
}
