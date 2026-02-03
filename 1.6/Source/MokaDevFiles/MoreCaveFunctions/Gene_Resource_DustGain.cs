using RimWorld;
using System.Collections.Generic;
using Verse;

namespace MokaDevSpace
{
    internal class Gene_Resource_DustGain : Gene, IGeneResourceDrain
    {
        [Unsaved(false)]
        private Gene_Resource_Dust cachedLumenGene;

        private const float MinAgeForDrain = 3f;

        public Gene_Resource Resource
        {
            get
            {
                if (cachedLumenGene == null || !cachedLumenGene.Active)
                {
                    cachedLumenGene = pawn.genes.GetFirstGeneOfType<Gene_Resource_Dust>();
                }
                return cachedLumenGene;
            }
        }

        public bool CanOffset
        {
            get
            {
                if (Active)
                {
                    if (pawn.Position.UsesOutdoorTemperature(pawn.Map))
                    {
                        return false;
                    }
                    return pawn.IsInCave();
                }
                return false;
            }
        }

        public float ResourceLossPerDay => def.resourceLossPerDay;

        public Pawn Pawn => pawn;

        public string DisplayLabel => Label + " (" + "Gene".Translate() + ")";

        public override void TickInterval(int delta)
        {
            base.TickInterval(delta);
            GeneResourceDrainUtility.TickResourceDrainInterval(this, delta);
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            if (!Active)
            {
                yield break;
            }
            //foreach (Gizmo resourceDrainGizmo in GeneResourceDrainUtility.GetResourceDrainGizmos(this))
            //{
            //    yield return resourceDrainGizmo;
            //}
        }
    }
}
