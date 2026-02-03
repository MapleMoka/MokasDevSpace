using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    public class HediffComp_NearPsykiks : HediffComp
    {
        public HediffCompProperties_NearPsykiks Props => (HediffCompProperties_NearPsykiks)props;

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (!base.Pawn.IsHashIntervalTick(120))
            {
                return;
            }
            int num = CountNearbyPsykiks(parent.pawn);

            if (num > 10) num = 10;

            float rate = 1 + ((float)num / 10);
            parent.Severity = rate;
        }

        public int CountNearbyPsykiks(Pawn pawn)
        {
            Map thismap = pawn.MapHeld;
            if (thismap == null) { return -1; }
            float radius = Props.appliedRadius;
            IEnumerable<IntVec3> enumerable = GenRadial.RadialCellsAround(pawn.PositionHeld, radius, useCenter: true);
            List<Thing> list = new List<Thing>();
            foreach (IntVec3 item in enumerable)
            {
                if (item.InBounds(thismap) && !item.Fogged(thismap))
                {
                    Pawn nearPawn = item.GetFirstPawn(thismap);
                    if (nearPawn != null)
                    {
                        list.Add(nearPawn);
                    }
                }
            }
            float num = 0f;
            foreach (Pawn pawn1 in list) 
            {
                if (pawn1.genes != null && pawn1.HasRealGene(MCM_DefOf.Moka_Psykicks))
                {
                    num += 1f;
                }
            }
            return (int)num;
        }
    }
}