using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace MokaDevSpace
{
    public class Gene_Resource_Pollution : Gene_Resource, IGeneResourceDrain
    {
        private int lastConsumed;

        public Gene_Resource Resource => this;

        public bool CanOffset => pawn.Spawned && Active;

        public float ResourceLossPerDay => def.resourceLossPerDay;

        public Pawn Pawn => pawn;

        public string DisplayLabel => def.resourceLabel;

        public override float InitialResourceMax => 1f;

        public override float MinLevelForAlert => 0.15f;

        public override float MaxLevelOffset => base.MaxLevelOffset;

        protected override Color BarColor => new Color(0.53f, 0.53f, 0.35f);

        protected override Color BarHighlightColor => new Color(0.6f, 0.67f, 0.4f);
        private float PollGainInToxGasPerHour = 0.005f;
        private float PollGainInPollutedTerrainPerDay = 0.25f;
        private float PollGainFromToxBuildupPerHour = 0.0025f;
        private float PollGainFromWastepack = 0.15f;
        private float PollGainFromAbsorbedPollution = 0.50f;
        private float PollGainPerToxibleIngested = 0.25f;
        private float PollGainPerToxibleInjected = 0.45f;

        private float PollLossPerAbilityUsed = 0.001f;
        public float PollThreshholdForSkills = 0.75f;

        private float lastDelta;
        private const float ThresholdExtremelyNegative = 0.05f;
        private const float ThresholdVeryNegative = 0.2f;
        private const float ThresholdNegative = 0.35f;
        private const float ThresholdNeutral = 0.65f;
        private const float ThresholdPositive = 0.8f;
        private const float ThresholdVeryPositive = 0.95f;
        private const float TicksPerDay = 60000f;
        private const float TicksPerHour = 2500f;
        private const float TicksPerInterval = 150f;

        //private bool PawnIsPyromaniac => this.pawn.story.traits.HasTrait(TraitDefOf.Pyromaniac);
        private bool PawnIsIrradiant => this.pawn.HasRealGene(MCM_DefOf.Moka_NeedPollution);

        //private float FallPerNeedIntervalTick
        //{
            //get => (float)(150.0 * (double)this.def.fallPerDay / 60000.0);
        //}
        private float PollGainInToxGasPerNeedIntervalTick
        {
            get => (float)(150.0 * (double)this.PollGainInToxGasPerHour / 2500.0);
        }
        private float PollGainInPollutedTerrainPerNeedIntervalTick
        {
            get => (float)(150.0 * (double)this.PollGainInPollutedTerrainPerDay / 60000.0);
        }
        public float PollGainFromToxBuildupPerNeedIntervalTick
        {
            get => (float)(150.0 * (double)this.PollGainFromToxBuildupPerHour / 2500.0);
        }
        private float PollLossPerAbilityUsedPerNeedIntervalTick
        {
            get => (float)(150.0 * (double)this.PollLossPerAbilityUsed / 10000.0);
        }

        public override bool Active
        {
            get
            {
                if (base.Active && pawn != null)
                {
                    return !pawn.IsGhoul;
                }
                return false;
            }
        }

        public bool ShouldConsumeNow()
        {
            if (!Active)
            {
                return false;
            }
            return Value < 0.1f;
        }

        public override void TickInterval(int delta)
        {
            base.TickInterval(delta);
            GeneResourceDrainUtility.TickResourceDrainInterval(this, delta);
            if (Resource.Value <= 0f && pawn.IsHashIntervalTick(60, delta) && pawn.health.hediffSet?.GetFirstHediffOfDef(MCM_DefOf.Moka_PollutionCraving) == null)
            {
                pawn.health.AddHediff(MCM_DefOf.Moka_PollutionCraving);
            }
            PollutionChecks(this.pawn);

        }

        public void PollutionChecks(Pawn pawn)
        {
            //if (pawn.OnPollutedTile())
            //{
            //    Resource.Value += this.PollGainInPollutedTerrainPerNeedIntervalTick;
            //}
            //if (pawn.InToxGas())
            //{
            //    Resource.Value += this.PollGainInToxGasPerNeedIntervalTick;
            //}
            //if (pawn.HasToxicBuildup())
            //{
            //    Resource.Value += this.PollGainFromToxBuildupPerNeedIntervalTick;
            //}
        }

        public void PollutedTileChange()
        {
            Resource.Value += this.lastDelta = +this.PollGainInPollutedTerrainPerNeedIntervalTick;
        }
        public void ToxGasChange()
        {
            Resource.Value += this.lastDelta = +this.PollGainInToxGasPerNeedIntervalTick;
        }
        public void ToxBuildupChange()
        {
            Resource.Value += this.lastDelta = +this.PollGainFromToxBuildupPerNeedIntervalTick;
        }
        public void ToxAilityUsed()
        {
            Resource.Value += this.lastDelta = -this.PollLossPerAbilityUsedPerNeedIntervalTick;
        }

        public override void Notify_IngestedThing(Thing thing, int numTaken)
        {
            if (thing.def == ThingDefOf.InsectJelly)
            {
                Value += 0.5f;
                lastConsumed = Find.TickManager.TicksGame;
            }
            if (thing.def.IsMeat)
            {
                IngestibleProperties ingestible = thing.def.ingestible;
                if (ingestible != null && ingestible.sourceDef?.race?.Humanlike == true)
                {
                    Util_PollutionUtils.OffsetPollution(pawn, 0.0375f * thing.GetStatValue(StatDefOf.Nutrition) * (float)numTaken);
                }
            }
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            if (!Active)
            {
                yield break;
            }
            foreach (Gizmo gizmo in base.GetGizmos())
            {
                yield return gizmo;
            }
            foreach (Gizmo resourceDrainGizmo in GeneResourceDrainUtility.GetResourceDrainGizmos(this))
            {
                yield return resourceDrainGizmo;
            }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref lastConsumed, "lastConsumed", 0);
        }





        //public bool hemogenPacksAllowed = true;

        public override void PostAdd()
        {
            if (ModLister.CheckBiotech("Hemogen"))
            {
                base.PostAdd();
                Reset();
            }
        }


        public override void SetTargetValuePct(float val)
        {
            targetValue = Mathf.Clamp(val * Max, 0f, Max - MaxLevelOffset);
        }

    }
}
