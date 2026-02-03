using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_PhotoRegeneration : HediffComp
    {
        private int ticksInLight = 0;

        public int solarDaysInTicks => this.Props.solarDays * 60000;

        public HediffCompProperties_PhotoRegeneration Props => (HediffCompProperties_PhotoRegeneration)this.props;

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look<int>(ref this.Props.solarDays, "solarDays");

        }

        public override void CompPostTick(ref float severityAdjustment)
        {

            base.CompPostTick(ref severityAdjustment);
            ++this.ticksInLight;

            if (this.ticksInLight < this.solarDaysInTicks)
                return;

            this.ticksInLight = 0;

            Pawn pawn = this.parent.pawn;

            if (!pawn.Spawned)
                return;

            if (pawn.PawnInLight())
            {
                Hediff_MissingPart firstHediff = pawn.health.hediffSet.GetFirstHediff<Hediff_MissingPart>();
                if (firstHediff != null)
                {
                    //Messages.Message("Moka_Regrowing".Translate(pawn, firstHediff.Part.LabelCap), MessageTypeDefOf.PositiveEvent, true);
                    HealthUtility.Cure((Hediff)firstHediff);
                }
            }

        }
    }
}
