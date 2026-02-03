using RimWorld;
using System.Collections.Generic;
using Verse;

namespace MokaDevSpace
{
    internal class Gene_Resource_ToxonFeeding : Gene //, IGeneResourceDrain
    {
        [Unsaved(false)]
        private Gene_Resource_Toxon cachedToxonGene;

        private const float MinAgeForDrain = 3f;

        public Gene_Resource Resource
        {
            get
            {
                if (cachedToxonGene == null || !cachedToxonGene.Active)
                {
                    cachedToxonGene = pawn.genes.GetFirstGeneOfType<Gene_Resource_Toxon>();
                }
                return cachedToxonGene;
            }
        }
        private Gene_Resource_Toxon lightGene => cachedToxonGene ?? (cachedToxonGene = base.pawn.genes.GetFirstGeneOfType<Gene_Resource_Toxon>());


        public bool CanOffset
        {
            get
            {
                if (Active)
                {
                    return !pawn.Deathresting;
                }
                return false;
            }
        }

        //public float ResourceLossPerDay => def.resourceLossPerDay;

        public Pawn Pawn => pawn;

        public string DisplayLabel => Label + " (" + "Gene".Translate() + ")";

        public override void TickInterval(int delta)
        {
            base.TickInterval(delta);
            //GeneResourceDrainUtility.TickResourceDrainInterval(this, delta);

        }

        public bool ShouldConsumeNow()
        {
            if (!Active)
            {
                return false;
            }
            return lightGene.Value < 0.5f;
        }
        public void ValueChange(float difference)
        {
            cachedToxonGene.Value += difference;
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
