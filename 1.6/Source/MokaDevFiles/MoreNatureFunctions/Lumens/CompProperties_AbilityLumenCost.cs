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
    public class CompProperties_AbilityLumenCost : CompProperties_AbilityEffect
    {
        public float lumenCost;

        public CompProperties_AbilityLumenCost()
        {
            compClass = typeof(CompAbilityEffect_LumenCost);
        }

        public override IEnumerable<string> ExtraStatSummary()
        {
            yield return string.Concat("AbilityLumenCost".Translate() + ": ", Mathf.RoundToInt(lumenCost * 100f).ToString());
        }
    }

}
