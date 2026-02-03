using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace MokaDevSpace
{
    public class Gene_Resource_Nanites : Gene_Resource, IGeneResourceDrain
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

        protected override Color BarColor => new Color(0.40f, 0.66f, 0.86f);

        protected override Color BarHighlightColor => new Color(0.45f, 0.78f, 0.94f);

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
                    Util_MechMethods.OffsetNanites(pawn, 0.0375f * thing.GetStatValue(StatDefOf.Nutrition) * (float)numTaken);
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
        //public bool nanitePacksAllowed = true;

        public override void PostAdd()
        {
            if (ModLister.CheckBiotech("Nanite"))
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
