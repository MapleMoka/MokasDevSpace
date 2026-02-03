using RimWorld;
using System.Collections.Generic;
using Verse;

namespace MokaDevSpace
{
    public class CompAbilityEffect_NaniteCost : CompAbilityEffect
    {
        public List<AbilityComp> comps;
        public new CompProperties_AbilityNaniteCost Props => (CompProperties_AbilityNaniteCost)props;

        private bool HasEnoughNanite
        {
            get
            {
                Gene_Resource_Nanites gene_Nanite = parent.pawn.genes?.GetFirstGeneOfType<Gene_Resource_Nanites>();
                if (gene_Nanite == null || gene_Nanite.Value < Props.naniteCost)
                {
                    return false;
                }
                return true;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            Util_MechMethods.OffsetNanites(parent.pawn, 0f - Props.naniteCost);
        }

        public override bool GizmoDisabled(out string reason)
        {
            Gene_Resource_Nanites gene_Nanite = parent.pawn.genes?.GetFirstGeneOfType<Gene_Resource_Nanites>();
            if (gene_Nanite == null)
            {
                reason = "AbilityDisabledNoNaniteGene".Translate(parent.pawn);
                return true;
            }
            if (gene_Nanite.Value < Props.naniteCost)
            {
                reason = "AbilityDisabledNoNanite".Translate(parent.pawn);
                return true;
            }
            float num = TotalNaniteCostOfQueuedAbilities();
            float num2 = Props.naniteCost + num;
            if (Props.naniteCost > float.Epsilon && num2 > gene_Nanite.Value)
            {
                reason = "AbilityDisabledNoNanite".Translate(parent.pawn);
                return true;
            }
            reason = null;
            return false;
        }

        public override bool AICanTargetNow(LocalTargetInfo target)
        {
            return HasEnoughNanite;
        }

        private float TotalNaniteCostOfQueuedAbilities()
        {
            float num;
            if ((!(parent.pawn.jobs?.curJob?.verbToUse is Verb_CastAbility verb_CastAbility)))
            {
                num = (0f);
            }
            else
            {
                if (this.NaniteCost() == 0f)
                {
                    num = 0f;
                }
                num = this.NaniteCost();
            }

            if (parent.pawn.jobs != null)
            {
                for (int i = 0; i < parent.pawn.jobs.jobQueue.Count; i++)
                {
                    if (parent.pawn.jobs.jobQueue[i].job.verbToUse is Verb_CastAbility verb_CastAbility2)
                    {
                        if (this.NaniteCost() == 0f)
                        {
                            num += 0f;
                        }
                        num += this.NaniteCost();
                    }
                }
            }
            return num;
        }
        public float NaniteCost()
        {
            if (comps != null)
            {
                foreach (AbilityComp comp in comps)
                {
                    if (comp is CompAbilityEffect_NaniteCost compAbilityEffect_NaniteCost)
                    {
                        return Props.naniteCost;
                    }
                }
            }
            return 0f;
        }
    }
}
