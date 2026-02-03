using RimWorld;
using System.Collections.Generic;
using Verse;

namespace MokaDevSpace
{
    internal class Gene_Resource_LumenSolar : Gene, IGeneResourceDrain
    {
        [Unsaved(false)]
        private Gene_Resource_Lumens cachedLumenGene;

        private const float MinAgeForDrain = 3f;

        public Gene_Resource Resource
        {
            get
            {
                if (cachedLumenGene == null || !cachedLumenGene.Active)
                {
                    cachedLumenGene = pawn.genes.GetFirstGeneOfType<Gene_Resource_Lumens>();
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
                    return pawn.PawnInLight();
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
