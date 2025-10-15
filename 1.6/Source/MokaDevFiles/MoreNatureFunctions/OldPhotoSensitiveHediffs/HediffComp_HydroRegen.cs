using System.Collections.Generic;
using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_HydroRegen : HediffComp
    {
        private int ticksWhileWet = 0;

        public int wetHoursInTicks => this.Props.wetHours * 2500;

        public HediffCompProperties_HydroRegen Props => (HediffCompProperties_HydroRegen)this.props;

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look<int>(ref this.Props.wetHours, "wetHours");

        }

        public override void CompPostTick(ref float severityAdjustment)
        {

            base.CompPostTick(ref severityAdjustment);

            ++this.ticksWhileWet;

            if (this.ticksWhileWet < this.wetHoursInTicks)
                return;

            this.ticksWhileWet = 0;

            Pawn pawn = this.parent.pawn;

            if (!pawn.Spawned)
                return;

            if (pawn.PawnIsWet() && pawn.health != null)
            {
                List<Hediff_Injury> injuries = this.GetInjuries(pawn);
                if (injuries.Count > 0)
                {
                    injuries.RandomElement<Hediff_Injury>().Severity -= this.Props.healAmount;
                }

            }
        }

        public List<Hediff_Injury> GetInjuries(Pawn pawn)
        {
            List<Hediff_Injury> injuries = new List<Hediff_Injury>();
            for (int index = 0; index < pawn.health.hediffSet.hediffs.Count; ++index)
            {
                if (pawn.health.hediffSet.hediffs[index] is Hediff_Injury hediff)
                    injuries.Add(hediff);
            }
            return injuries;
        }
    }
}
