using MCM;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace MokaDevSpace
{
    public class Need_Pollution : Need
    {
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

        private float FallPerNeedIntervalTick
        {
            get => (float)(150.0 * (double)this.def.fallPerDay / 60000.0);
        }
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

        private float ContainedGainPerNeedIntervalTick => 0.0072f;

        private float UncontainedGainPerNeedIntervalTick => 0.012f;

        public Need_Pollution.MoodLevels EffectiveMoodShiftPerLevel
        {
            get
            {
                if (this.Disabled)
                    return Need_Pollution.MoodLevels.Inactive;

                float curLevel = this.CurLevel;
                if ((double)curLevel < 0.05000000074505806)
                    return Need_Pollution.MoodLevels.Depleted;
                if ((double)curLevel < 0.20000000298023224)
                    return Need_Pollution.MoodLevels.Diminished;
                if ((double)curLevel < 0.40000000596046448)
                    return Need_Pollution.MoodLevels.Stable;
                if ((double)curLevel < 0.60000002384185791)
                    return Need_Pollution.MoodLevels.Refined;
                return (double)curLevel < 0.800000011920929 ? Need_Pollution.MoodLevels.Enriched : Need_Pollution.MoodLevels.Radiant;
            }
        }
        public override int GUIChangeArrow
        {
            get
            {
                if (this.IsFrozen)
                    return 0;
                return (double)this.lastDelta < 0.0 ? -1 : ((double)this.lastDelta > 0.0 ? 1 : 0);
            }
        }

        public override bool ShowOnNeedList => !this.Disabled;

        private bool Disabled => !this.PawnIsIrradiant;

        //public Pawn Pawn { get; } = pawn;

        public Need_Pollution(Pawn pawn)
            : base(pawn)
        {
            this.threshPercents = new List<float>();
            this.threshPercents.Add(0.8f);
            this.threshPercents.Add(0.6f);
            this.threshPercents.Add(0.4f);
            this.threshPercents.Add(0.2f);
            //this.threshPercents.Add(0.05f);
        }

        public override void SetInitialLevel() => this.CurLevel = 0.5f;

        public override void NeedInterval()
        {
            if (this.Disabled)
            {
                this.SetInitialLevel();
            }
            else
            {
                if (pawn.OnPollutedTile())
                {
                    this.PollutedTileChange();
                    return;
                }

                if (this.IsFrozen)
                    return;

                this.CurLevel += this.lastDelta = -this.FallPerNeedIntervalTick;
            }
        }

        public void PollutedTileChange()
        {
            this.CurLevel += this.lastDelta = +this.PollGainInPollutedTerrainPerNeedIntervalTick;
        }
        public void ToxGasChange()
        {
            this.CurLevel += this.lastDelta = +this.PollGainInToxGasPerNeedIntervalTick;
        }
        public void ToxBuildupChange()
        {
            this.CurLevel += this.lastDelta = +this.PollGainFromToxBuildupPerNeedIntervalTick;
        }
        public void ToxAilityUsed()
        {
            this.CurLevel += this.lastDelta = -this.PollLossPerAbilityUsedPerNeedIntervalTick;
        }

        public enum MoodLevels
        {
            Radiant,
            Enriched,
            Refined,
            Stable,
            Inactive,
            Diminished,
            Depleted
        }
    }
}
