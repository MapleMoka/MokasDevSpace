using RimWorld;
using System.Collections.Generic;
using Verse;

namespace MokaDevSpace
{
    public class CompAbilityEffect_LumenCost : CompAbilityEffect
    {
        public List<AbilityComp> comps;
        public new CompProperties_AbilityLumenCost Props => (CompProperties_AbilityLumenCost)props;

        private bool HasEnoughLumen
        {
            get
            {
                Gene_Resource_Lumens gene_Lumen = parent.pawn.genes?.GetFirstGeneOfType<Gene_Resource_Lumens>();
                if (gene_Lumen == null || gene_Lumen.Value < Props.lumenCost)
                {
                    return false;
                }
                return true;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Utils_MapBasedEffects.OffsetLumens(parent.pawn, 0f - Props.lumenCost);
        }

        public override bool GizmoDisabled(out string reason)
        {
            Gene_Resource_Lumens gene_Lumen = parent.pawn.genes?.GetFirstGeneOfType<Gene_Resource_Lumens>();
            if (gene_Lumen == null)
            {
                reason = "AbilityDisabledNoLumenGene".Translate(parent.pawn);
                return true;
            }
            if (gene_Lumen.Value < Props.lumenCost)
            {
                reason = "AbilityDisabledNoLumen".Translate(parent.pawn);
                return true;
            }
            float num = TotalLumenCostOfQueuedAbilities();
            float num2 = Props.lumenCost + num;
            if (Props.lumenCost > float.Epsilon && num2 > gene_Lumen.Value)
            {
                reason = "AbilityDisabledNoLumen".Translate(parent.pawn);
                return true;
            }
            reason = null;
            return false;
        }

        public override bool AICanTargetNow(LocalTargetInfo target)
        {
            return HasEnoughLumen;
        }

        private float TotalLumenCostOfQueuedAbilities()
        {
            float num;
            if ((!(parent.pawn.jobs?.curJob?.verbToUse is Verb_CastAbility verb_CastAbility)))
            {
                num = (0f);
            }
            else
            {
                if (this.LumenCost() == 0f)
                {
                    num = 0f;
                }
                num = this.LumenCost();
            }

            if (parent.pawn.jobs != null)
            {
                for (int i = 0; i < parent.pawn.jobs.jobQueue.Count; i++)
                {
                    if (parent.pawn.jobs.jobQueue[i].job.verbToUse is Verb_CastAbility verb_CastAbility2)
                    {
                        if (this.LumenCost() == 0f)
                        {
                            num += 0f;
                        }
                        num += this.LumenCost();
                    }
                }
            }
            return num;
        }
        public float LumenCost()
        {
            if (comps != null)
            {
                foreach (AbilityComp comp in comps)
                {
                    if (comp is CompAbilityEffect_LumenCost compAbilityEffect_LumenCost)
                    {
                        return Props.lumenCost;
                    }
                }
            }
            return 0f;
        }
    }
}
