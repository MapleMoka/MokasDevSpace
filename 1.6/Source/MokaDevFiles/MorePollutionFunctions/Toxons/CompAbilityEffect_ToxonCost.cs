using RimWorld;
using System.Collections.Generic;
using Verse;

namespace MokaDevSpace
{
    public class CompAbilityEffect_ToxonCost : CompAbilityEffect
    {
        public List<AbilityComp> comps;
        public new CompProperties_AbilityToxonCost Props => (CompProperties_AbilityToxonCost)props;

        private bool HasEnoughToxon
        {
            get
            {
                Gene_Resource_Toxon gene_Toxon = parent.pawn.genes?.GetFirstGeneOfType<Gene_Resource_Toxon>();
                if (gene_Toxon == null || gene_Toxon.Value < Props.toxonCost)
                {
                    return false;
                }
                return true;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Util_PollutionUtils.OffsetToxons(parent.pawn, 0f - Props.toxonCost);
        }

        public override bool GizmoDisabled(out string reason)
        {
            Gene_Resource_Toxon gene_Toxon = parent.pawn.genes?.GetFirstGeneOfType<Gene_Resource_Toxon>();
            if (gene_Toxon == null)
            {
                reason = "AbilityDisabledNoToxonGene".Translate(parent.pawn);
                return true;
            }
            if (gene_Toxon.Value < Props.toxonCost)
            {
                reason = "AbilityDisabledNoToxon".Translate(parent.pawn);
                return true;
            }
            float num = TotalToxonCostOfQueuedAbilities();
            float num2 = Props.toxonCost + num;
            if (Props.toxonCost > float.Epsilon && num2 > gene_Toxon.Value)
            {
                reason = "AbilityDisabledNoToxon".Translate(parent.pawn);
                return true;
            }
            reason = null;
            return false;
        }

        public override bool AICanTargetNow(LocalTargetInfo target)
        {
            return HasEnoughToxon;
        }

        private float TotalToxonCostOfQueuedAbilities()
        {
            float num;
            if ((!(parent.pawn.jobs?.curJob?.verbToUse is Verb_CastAbility verb_CastAbility)))
            {
                num = (0f);
            }
            else
            {
                if (this.ToxonCost() == 0f)
                {
                    num = 0f;
                }
                num = this.ToxonCost();
            }

            if (parent.pawn.jobs != null)
            {
                for (int i = 0; i < parent.pawn.jobs.jobQueue.Count; i++)
                {
                    if (parent.pawn.jobs.jobQueue[i].job.verbToUse is Verb_CastAbility verb_CastAbility2)
                    {
                        if (this.ToxonCost() == 0f)
                        {
                            num += 0f;
                        }
                        num += this.ToxonCost();
                    }
                }
            }
            return num;
        }
        public float ToxonCost()
        {
            if (comps != null)
            {
                foreach (AbilityComp comp in comps)
                {
                    if (comp is CompAbilityEffect_ToxonCost compAbilityEffect_ToxonCost)
                    {
                        return Props.toxonCost;
                    }
                }
            }
            return 0f;
        }
    }
}
