using RimWorld;
using System.Collections.Generic;
using Verse;

namespace MokaDevSpace
{
    public class CompAbilityEffect_DustCost : CompAbilityEffect
    {
        public List<AbilityComp> comps;
        public new CompProperties_AbilityDustCost Props => (CompProperties_AbilityDustCost)props;

        private bool HasEnoughDust
        {
            get
            {
                Gene_Resource_Dust gene_Dust = parent.pawn.genes?.GetFirstGeneOfType<Gene_Resource_Dust>();
                if (gene_Dust == null || gene_Dust.Value < Props.dustCost)
                {
                    return false;
                }
                return true;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Util_CaveMethods.OffsetDust(parent.pawn, 0f - Props.dustCost);
        }

        public override bool GizmoDisabled(out string reason)
        {
            Gene_Resource_Dust gene_Dust = parent.pawn.genes?.GetFirstGeneOfType<Gene_Resource_Dust>();
            if (gene_Dust == null)
            {
                reason = "AbilityDisabledNoDustGene".Translate(parent.pawn);
                return true;
            }
            if (gene_Dust.Value < Props.dustCost)
            {
                reason = "AbilityDisabledNoDust".Translate(parent.pawn);
                return true;
            }
            float num = TotalDustCostOfQueuedAbilities();
            float num2 = Props.dustCost + num;
            if (Props.dustCost > float.Epsilon && num2 > gene_Dust.Value)
            {
                reason = "AbilityDisabledNoDust".Translate(parent.pawn);
                return true;
            }
            reason = null;
            return false;
        }

        public override bool AICanTargetNow(LocalTargetInfo target)
        {
            return HasEnoughDust;
        }

        private float TotalDustCostOfQueuedAbilities()
        {
            float num;
            if ((!(parent.pawn.jobs?.curJob?.verbToUse is Verb_CastAbility verb_CastAbility)))
            {
                num = (0f);
            }
            else
            {
                if (this.DustCost() == 0f)
                {
                    num = 0f;
                }
                num = this.DustCost();
            }

            if (parent.pawn.jobs != null)
            {
                for (int i = 0; i < parent.pawn.jobs.jobQueue.Count; i++)
                {
                    if (parent.pawn.jobs.jobQueue[i].job.verbToUse is Verb_CastAbility verb_CastAbility2)
                    {
                        if (this.DustCost() == 0f)
                        {
                            num += 0f;
                        }
                        num += this.DustCost();
                    }
                }
            }
            return num;
        }
        public float DustCost()
        {
            if (comps != null)
            {
                foreach (AbilityComp comp in comps)
                {
                    if (comp is CompAbilityEffect_DustCost compAbilityEffect_DustCost)
                    {
                        return Props.dustCost;
                    }
                }
            }
            return 0f;
        }
    }
}
