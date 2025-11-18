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
    public class CompProperties_AbilityPollutionCost : CompProperties_AbilityEffect
    {
        public float hemogenCost;

        public CompProperties_AbilityPollutionCost()
        {
            compClass = typeof(CompAbilityEffect_PollutionCost);
        }

        public override IEnumerable<string> ExtraStatSummary()
        {
            yield return string.Concat("AbilityHemogenCost".Translate() + ": ", Mathf.RoundToInt(hemogenCost * 100f).ToString());
        }
    }

}
