using RimWorld;
//using VanillaRacesExpandedPhytokin;
using Verse;
using Verse.AI;

namespace MokaDevSpace
{
    public class JobGiver_ConsumeWastepack : ThinkNode_JobGiver
    {
        public override float GetPriority(Pawn pawn)
        {
            if (!ModsConfig.BiotechActive)
            {
                return 0f;
            }
            //if (!pawn.HasRealGene(MCM_DefOf.Moka_ToxonFeeding))
            //{
            //    return 0f;
            //}
            Pawn_GeneTracker genes = pawn.genes;
            if (genes != null && genes.GetFirstGeneOfType<Gene_Resource_ToxonFeeding>() == null)
            {
                return 0f;
            }
            return 9.1f;
        }

        protected override Job TryGiveJob(Pawn pawn)
        {
            if (!ModsConfig.BiotechActive)
            {
                return null;
            }
            //if (!pawn.HasRealGene(MCM_DefOf.Moka_ToxonFeeding))
            //{
            //    return null;
            //}
            Gene_Resource_ToxonFeeding firstGeneOfType = pawn.genes.GetFirstGeneOfType<Gene_Resource_ToxonFeeding>();
            if (firstGeneOfType == null)
            {
                return null;
            }
            if (!firstGeneOfType.ShouldConsumeNow())
            {
                return null;
            }
            Thing wastepack = GetWastepack(pawn);
            if (wastepack == null)
            {
                return null;
            }
            Job job = JobMaker.MakeJob(MCM_DefOf.Moka_ConsumeWastepack, wastepack);
            job.count = 1;
            return job;
        }

        public static Thing GetWastepack(Pawn pawn)
        {
            return GenClosest.ClosestThing_Global_Reachable(pawn.Position, pawn.Map, pawn.Map.listerThings.ThingsOfDef(ThingDefOf.Wastepack), PathEndMode.Touch, TraverseParms.For(pawn), 9999f, (Thing thing) => pawn.CanReserve(thing) && !thing.IsForbidden(pawn));
        }
    }

}
