using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace MokaDevSpace
{
    public class CompProperties_AbilityNaniteCost : CompProperties_AbilityEffect
    {
        public float naniteCost;

        public CompProperties_AbilityNaniteCost()
        {
            compClass = typeof(CompAbilityEffect_NaniteCost);
        }

        public override IEnumerable<string> ExtraStatSummary()
        {
            yield return string.Concat("AbilityNaniteCost".Translate() + ": ", Mathf.RoundToInt(naniteCost * 100f).ToString());
        }
    }

}
