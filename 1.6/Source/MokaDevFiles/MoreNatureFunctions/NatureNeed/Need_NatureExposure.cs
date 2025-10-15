using MCM;
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
    public class Need_NatureExposure : Need
    {
        private const float Delta_IndoorsThickRoof = -0.45f;
        private const float Delta_OutdoorsThickRoof = -0.4f;
        private const float Delta_IndoorsThinRoof = -0.32f;
        private const float Minimum_IndoorsThinRoof = 0.2f;
        private const float Delta_OutdoorsThinRoof = 1f;
        private const float Delta_IndoorsNoRoof = 5f;
        private const float Delta_OutdoorsNoRoof = 8f;
        private const float DeltaFactor_InBed = 0.2f;
        private float lastEffectiveDelta;

        public override int GUIChangeArrow => this.IsFrozen ? 0 : Math.Sign(this.lastEffectiveDelta);
        private bool PawnIsVerdant => this.pawn.HasRealGene(MCM_DefOf.Moka_NeedNatureExposure);

        public Need_NatureExposure.MoodLevels EffectiveMoodShiftPerLevel
        {
            get
            {
                if (this.Disabled)
                    return Need_NatureExposure.MoodLevels.Inactive;

                float curLevel = this.CurLevel;
                if ((double)curLevel < 0.05000000074505806)
                    return Need_NatureExposure.MoodLevels.Sickly;
                if ((double)curLevel < 0.20000000298023224)
                    return Need_NatureExposure.MoodLevels.Autumnal;
                if ((double)curLevel < 0.40000000596046448)
                    return Need_NatureExposure.MoodLevels.Foliant;
                if ((double)curLevel < 0.60000002384185791)
                    return Need_NatureExposure.MoodLevels.Mossy;
                return (double)curLevel < 0.800000011920929 ? Need_NatureExposure.MoodLevels.Lush : Need_NatureExposure.MoodLevels.Verdant;
            }
        }
        public override bool ShowOnNeedList => !this.Disabled;

        private bool Disabled => !this.PawnIsVerdant || this.pawn.Dead;

        public Need_NatureExposure(Pawn pawn)
          : base(pawn)
        {
            this.threshPercents = new List<float>();
            this.threshPercents.Add(0.8f);
            this.threshPercents.Add(0.6f);
            this.threshPercents.Add(0.4f);
            this.threshPercents.Add(0.2f);
            //this.threshPercents.Add(0.05f);
        }

        public override void SetInitialLevel() => this.CurLevel = 1f;

        public override void NeedInterval()
        {
            if (this.Disabled)
            {
                this.CurLevel = 1f;
            }
            else
            {
                if (this.IsFrozen)
                    return;
                float b = 0.2f;
                int num1 = !this.pawn.Spawned ? 1 : (this.pawn.Position.UsesOutdoorTemperature(this.pawn.Map) ? 1 : 0);
                RoofDef roof = this.pawn.Spawned ? this.pawn.Position.GetRoof(this.pawn.Map) : (RoofDef)null;
                float num2;
                if (num1 == 0)
                {
                    if (roof == null)
                        num2 = 5f;
                    else if (!roof.isThickRoof)
                    {
                        num2 = -0.32f;
                    }
                    else
                    {
                        num2 = -0.45f;
                        b = 0.0f;
                    }
                }
                else
                    num2 = roof != null ? (!roof.isThickRoof ? 1f : -0.4f) : 8f;
                if (this.pawn.InBed() && (double)num2 < 0.0)
                    num2 *= 0.2f;
                float num3 = num2 * (1f / 400f);
                float curLevel = this.CurLevel;
                if ((double)num3 < 0.0)
                    this.CurLevel = Mathf.Min(this.CurLevel, Mathf.Max(this.CurLevel + num3, b));
                else
                    this.CurLevel = Mathf.Min(this.CurLevel + num3, 1f);
                this.lastEffectiveDelta = this.CurLevel - curLevel;
            }
        }
        public enum MoodLevels
        {
            Verdant,
            Lush,
            Mossy,
            Inactive,
            Foliant,
            Autumnal,
            Sickly
        }
    }
}
