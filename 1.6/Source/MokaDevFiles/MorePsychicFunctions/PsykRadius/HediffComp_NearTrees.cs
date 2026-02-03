using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    public class HediffComp_NearTrees : HediffComp
    {
        public HediffCompProperties_NearTrees Props => (HediffCompProperties_NearTrees)props;

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (!base.Pawn.IsHashIntervalTick(120))
            {
                return;
            }
            int num = CountNearPlants(parent.pawn);

            if (num > 10) num = 10;

            float rate = 1 + ((float)num / 10);
            parent.Severity = rate;
        }
        public static int CountNearPlants(Pawn pawn)
        {
            Map mapHeld = pawn.MapHeld;
            if (mapHeld == null)
            {
                return -1;
            }
            float radius = 5f;
            IEnumerable<IntVec3> enumerable = GenRadial.RadialCellsAround(pawn.PositionHeld, radius, useCenter: true);
            List<Thing> list = new List<Thing>();
            foreach (IntVec3 item in enumerable)
            {
                if (item.InBounds(mapHeld) && !item.Fogged(mapHeld))
                {
                    Plant plant = item.GetPlant(mapHeld);
                    if (plant != null)
                    {
                        list.Add(plant);
                    }
                }
            }
            float num = 0f;
            foreach (Thing item2 in list)
            {
                num = (item2.def.plant.IsTree ? ((item2.def != ThingDefOf.Plant_TreeGauranlen && item2.def != ThingDefOf.Plant_TreeAnima) ? (item2.def.plant.isStump ? (num - 0.5f) : (num + 1f)) : (num + 15f)) : (num + 0.3f));
            }
            return (int)num;
        }
    }
}