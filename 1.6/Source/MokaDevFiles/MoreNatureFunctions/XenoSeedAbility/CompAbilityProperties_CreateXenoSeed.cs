using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class CompAbilityProperties_CreateXenoSeed : CompProperties_AbilityEffectWithDuration
    {
        public CompAbilityProperties_CreateXenoSeed()
        {
            this.compClass = typeof(CompAbilityEffect_CreateXenoSeed);
        }
        public HediffDef hediffDef;
        public ThingDef xenoSeed;
        public bool onlyBrain;
        public bool applyToSelf;
        public bool onlyApplyToSelf;
        public bool applyToTarget = true;
        public bool replaceExisting;
        public float severity = -1f;
        public bool ignoreSelf;
    }
}