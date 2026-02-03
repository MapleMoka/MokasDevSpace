using RimWorld;
using System.Collections.Generic;
using Verse;

namespace MokaDevSpace
{
    public class CompAbilityEffect_FungusCost : CompAbilityEffect
    {
        public List<AbilityComp> comps;
        public new CompProperties_AbilityFungusCost Props => (CompProperties_AbilityFungusCost)props;

        private bool HasEnoughFungus
        {
            get
            {
                Gene_Resource_Fungus gene_Fungus = parent.pawn.genes?.GetFirstGeneOfType<Gene_Resource_Fungus>();
                if (gene_Fungus == null || gene_Fungus.Value < Props.fungusCost)
                {
                    return false;
                }
                return true;
            }
        }

        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            //Util_CaveMethods.OffsetFungus(parent.pawn, 0f - Props.fungusCost);
        }

        public override bool GizmoDisabled(out string reason)
        {
            Gene_Resource_Fungus gene_Fungus = parent.pawn.genes?.GetFirstGeneOfType<Gene_Resource_Fungus>();
            if (gene_Fungus == null)
            {
                reason = "AbilityDisabledNoFungusGene".Translate(parent.pawn);
                return true;
            }
            if (gene_Fungus.Value < Props.fungusCost)
            {
                reason = "AbilityDisabledNoFungus".Translate(parent.pawn);
                return true;
            }
            float num = TotalFungusCostOfQueuedAbilities();
            float num2 = Props.fungusCost + num;
            if (Props.fungusCost > float.Epsilon && num2 > gene_Fungus.Value)
            {
                reason = "AbilityDisabledNoFungus".Translate(parent.pawn);
                return true;
            }
            reason = null;
            return false;
        }

        public override bool AICanTargetNow(LocalTargetInfo target)
        {
            return HasEnoughFungus;
        }

        private float TotalFungusCostOfQueuedAbilities()
        {
            float num;
            if ((!(parent.pawn.jobs?.curJob?.verbToUse is Verb_CastAbility verb_CastAbility)))
            {
                num = (0f);
            }
            else
            {
                if (this.FungusCost() == 0f)
                {
                    num = 0f;
                }
                num = this.FungusCost();
            }

            if (parent.pawn.jobs != null)
            {
                for (int i = 0; i < parent.pawn.jobs.jobQueue.Count; i++)
                {
                    if (parent.pawn.jobs.jobQueue[i].job.verbToUse is Verb_CastAbility verb_CastAbility2)
                    {
                        if (this.FungusCost() == 0f)
                        {
                            num += 0f;
                        }
                        num += this.FungusCost();
                    }
                }
            }
            return num;
        }
        public float FungusCost()
        {
            if (comps != null)
            {
                foreach (AbilityComp comp in comps)
                {
                    if (comp is CompAbilityEffect_FungusCost compAbilityEffect_FungusCost)
                    {
                        return Props.fungusCost;
                    }
                }
            }
            return 0f;
        }
    }
}
