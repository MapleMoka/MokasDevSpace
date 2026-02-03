using RimWorld;
using System.Collections.Generic;
using Verse;

namespace MokaDevSpace
{
    public class CompProperties_AbilityToxonSaturation : CompProperties_AbilityEffect
    {
        public GeneDef polluteGene;
        public List<HediffDef> geneDefs;
        public float severityOffsetBeneficial;
        public float severityOffsetDetrimental;

        public CompProperties_AbilityToxonSaturation()
        {
            compClass = typeof(CompAbilityEffect_ToxonSaturation);
        }

    }
}
