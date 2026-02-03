using RimWorld;
using System.Collections.Generic;
using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_FloralRegeneration : HediffComp
    {
        private int ticksInLight = 0;

        //public int solarDaysInTicks => this.Props.solarDays * 60000;

        public float healPointsSaved;
        public int daysToHealInTicks => (this.Props.daysToHeal * 60000)/2;
        public int daysToGenerateInTicks => this.Props.daysToGenerate * 60000;

        public HediffCompProperties_FloralRegeneration Props => (HediffCompProperties_FloralRegeneration)this.props;

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look<int>(ref this.Props.amountToHeal, "amountToHeal");
            Scribe_Values.Look<int>(ref this.Props.daysToHeal, "daysToHeal");
            Scribe_Values.Look<int>(ref this.Props.daysToGenerate, "daysToGenerate");
            //Scribe_Values.Look<int>(ref this.Props.solarDays, "solarDays");

        }

        public override void CompPostTick(ref float severityAdjustment)
        {

            base.CompPostTick(ref severityAdjustment);
            Pawn pawn = this.parent.pawn;
            if (pawn.PawnInLight())
            {
                ++this.ticksInLight;

                if (pawn.health != null)
                {
                    List<Hediff_Injury> injuries = this.GetAllInjuries(pawn);
                    BodyPartRecord firstMissingBodyPart = this.FindFirstMissingBodyPart(pawn);

                    if (injuries.Count > 0)
                    {
                        if (this.ticksInLight < this.daysToHealInTicks)
                            return;

                        injuries.RandomElement<Hediff_Injury>().Severity -= (this.Props.amountToHeal * 2);
                    }
                    else if (firstMissingBodyPart != null)
                    {
                        if (this.ticksInLight < this.daysToHealInTicks)
                            return;

                        pawn.health.RestorePart(firstMissingBodyPart);
                        int amount = (int)pawn.health.hediffSet.GetPartHealth(firstMissingBodyPart) - 1;
                        DamageInfo dinfo = new DamageInfo(DamageDefOf.Cut, (float)amount, 999f, hitPart: firstMissingBodyPart);
                        dinfo.SetAllowDamagePropagation(false);
                        pawn.TakeDamage(dinfo);
                    }
                    else
                    {
                        if (this.healPointsSaved < 20)
                            this.healPointsSaved += 0.1f;
                        return;
                    }
                }
            }
            if (this.healPointsSaved > 0.5)
                {

                List<Hediff_Injury> injuries = this.GetAllInjuries(pawn);
                BodyPartRecord firstMissingBodyPart = this.FindFirstMissingBodyPart(pawn);
                if (injuries.Count > 0)
                {

                    this.healPointsSaved -= 0.5f;

                    injuries.RandomElement<Hediff_Injury>().Severity -= this.Props.amountToHeal;
                }
                else if (firstMissingBodyPart != null)
                {
                    this.healPointsSaved -= 0.5f;

                    pawn.health.RestorePart(firstMissingBodyPart);
                    int amount = (int)pawn.health.hediffSet.GetPartHealth(firstMissingBodyPart) - 1;
                    DamageInfo dinfo = new DamageInfo(DamageDefOf.Cut, (float)amount, 999f, hitPart: firstMissingBodyPart);
                    dinfo.SetAllowDamagePropagation(false);
                    pawn.TakeDamage(dinfo);
                }
            }

            ticksInLight = 0;

        }

        public List<Hediff_Injury> GetAllInjuries(Pawn pawn)
        {
            List<Hediff_Injury> injuries = new List<Hediff_Injury>();
            for (int index = 0; index < pawn.health.hediffSet.hediffs.Count; ++index)
            {
                if (pawn.health.hediffSet.hediffs[index] is Hediff_Injury hediff)
                    injuries.Add(hediff);
            }
            return injuries;
        }

        public BodyPartRecord FindFirstMissingBodyPart(Pawn pawn)
        {
            BodyPartRecord firstMissingBodyPart = (BodyPartRecord)null;
            foreach (Hediff_MissingPart partsCommonAncestor in pawn.health.hediffSet.GetMissingPartsCommonAncestors())
            {
                if (!pawn.health.hediffSet.PartOrAnyAncestorHasDirectlyAddedParts(partsCommonAncestor.Part) && (firstMissingBodyPart == null || (double)partsCommonAncestor.Part.coverageAbsWithChildren > (double)firstMissingBodyPart.coverageAbsWithChildren))
                    firstMissingBodyPart = partsCommonAncestor.Part;
            }
            return firstMissingBodyPart;
        }
    }
}
