using RimWorld;
using Verse;

namespace MokaDevSpace
{
    public class CompAbilityEffect_ToxonSaturation : CompAbilityEffect
    {
        public new CompProperties_AbilityToxonSaturation Props => (CompProperties_AbilityToxonSaturation)props;

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Pawn pawn = parent.pawn;
             
            if (pawn == null)
            {
                return;
            }
            bool geneHediffFound = false;
            if (Props.geneDefs != null)
            {
                foreach (HediffDef geneHediff in Props.geneDefs)
                {
                    //if (geneHediff == HediffDefOf.PollutionStimulus)
                    //{
                    //    if (Wearer.genes.GetFirstGeneOfType<Gene_PollutionRush>() != null && !Wearer.health.hediffSet.HasHediff(geneHediff))
                    //    {
                    //        Hediff hediff = HediffMaker.MakeHediff(geneHediff, Wearer);
                    //        hediff.Severity = Props.wasterHediffSeverityOffset;
                    //        Wearer.health.AddHediff(hediff);
                    //    }
                    //}
                    if (pawn.health.hediffSet.HasHediff(geneHediff))
                    {
                        geneHediffFound = true;
                        Hediff tempHediff = pawn.health.hediffSet.GetFirstHediffOfDef(geneHediff);
                        tempHediff.Severity = Props.severityOffsetBeneficial;
                    }
                }
                if (!geneHediffFound)
                {
                    AddAHediff(HediffDefOf.ToxicBuildup, Props.severityOffsetDetrimental, pawn);
                }
            }
            if (pawn.genes.HasActiveGene(Props.polluteGene) && !pawn.health.hediffSet.HasHediff(HediffDefOf.PollutionStimulus))
            {
                AddAHediff(HediffDefOf.PollutionStimulus, Props.severityOffsetBeneficial, pawn);
            }
        }
        public void AddAHediff(HediffDef hediffToAdd, float severity, Pawn pawn)
        {
            Hediff hediff = HediffMaker.MakeHediff(hediffToAdd, pawn);
            hediff.Severity = severity;
            pawn.health.AddHediff(hediff);
        }
    }
}
