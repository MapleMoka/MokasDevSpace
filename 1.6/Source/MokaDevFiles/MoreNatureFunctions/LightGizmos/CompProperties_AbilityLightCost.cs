using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace MokaDevSpace
{
    public class CompProperties_AbilityLightCost : CompProperties_AbilityEffect
    {
        public float hemogenCost;

        public CompProperties_AbilityLightCost()
        {
            compClass = typeof(CompAbilityEffect_LightCost);
        }

        public override IEnumerable<string> ExtraStatSummary()
        {
            yield return string.Concat("AbilityHemogenCost".Translate() + ": ", Mathf.RoundToInt(hemogenCost * 100f).ToString());
        }
    }

}
