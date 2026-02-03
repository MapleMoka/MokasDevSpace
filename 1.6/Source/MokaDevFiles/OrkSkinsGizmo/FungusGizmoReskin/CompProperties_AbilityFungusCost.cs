using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace MokaDevSpace
{
    public class CompProperties_AbilityFungusCost : CompProperties_AbilityEffect
    {
        public float fungusCost;

        public CompProperties_AbilityFungusCost()
        {
            compClass = typeof(CompAbilityEffect_FungusCost);
        }

        public override IEnumerable<string> ExtraStatSummary()
        {
            yield return string.Concat("AbilityFungusCost".Translate() + ": ", Mathf.RoundToInt(fungusCost * 100f).ToString());
        }
    }

}
