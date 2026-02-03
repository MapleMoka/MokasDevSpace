using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace MokaDevSpace
{
    public class Gene_Resource_Toxon : Gene_Resource, IGeneResourceDrain
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

        protected override Color BarColor => new Color(0.52f, 0.70f, 0.35f);

        protected override Color BarHighlightColor => new Color(0.60f, 0.80f, 0.40f);

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

        public override void TickInterval(int delta)
        {
            base.TickInterval(delta);
            GeneResourceDrainUtility.TickResourceDrainInterval(this, delta);
            if (Resource.Value <= 0f && pawn.IsHashIntervalTick(60, delta) && pawn.health.hediffSet?.GetFirstHediffOfDef(MCM_DefOf.Moka_ToxonCraving) == null)
            {
                pawn.health.AddHediff(MCM_DefOf.Moka_ToxonCraving);
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
        //public bool toxonPacksAllowed = true;

        public override void PostAdd()
        {
            if (ModLister.CheckBiotech("Toxon"))
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
