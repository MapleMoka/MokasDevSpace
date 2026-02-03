using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace MokaDevSpace
{
    public class CompProperties_AbilityToxonCost : CompProperties_AbilityEffect
    {
        public float toxonCost;

        public CompProperties_AbilityToxonCost()
        {
            compClass = typeof(CompAbilityEffect_ToxonCost);
        }

        public override IEnumerable<string> ExtraStatSummary()
        {
            yield return string.Concat("AbilityToxonCost".Translate() + ": ", Mathf.RoundToInt(toxonCost * 100f).ToString());
        }
    }

}
