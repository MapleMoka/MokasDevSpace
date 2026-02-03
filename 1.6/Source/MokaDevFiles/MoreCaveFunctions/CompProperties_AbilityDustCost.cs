using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace MokaDevSpace
{
    public class CompProperties_AbilityDustCost : CompProperties_AbilityEffect
    {
        public float dustCost;

        public CompProperties_AbilityDustCost()
        {
            compClass = typeof(CompAbilityEffect_DustCost);
        }

        public override IEnumerable<string> ExtraStatSummary()
        {
            yield return string.Concat("AbilityDustCost".Translate() + ": ", Mathf.RoundToInt(dustCost * 100f).ToString());
        }
    }

}
