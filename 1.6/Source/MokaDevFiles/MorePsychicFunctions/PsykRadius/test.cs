using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace.MorePsychicFunctions.PsykRadius
{
    public class Comp_NatureConnectionHediff : HediffComp
    {
        public override void CompPostTick(ref float severityAdjustment)
        {
            if (!base.Pawn.IsHashIntervalTick(120))
            {
                return;
            }
            int num = CountNearPlants(parent.pawn);
            int num2 = num;
            int num3 = num2;
            float severity;
            if (num3 < 5)
            {
                severity = 0.5f;
            }
            else
            {
                int num4 = num3;
                if (num4 < 10)
                {
                    severity = 1f;
                }
                else
                {
                    int num5 = num3;
                    if (num5 < 15)
                    {
                        severity = 2f;
                    }
                    else
                    {
                        int num6 = num3;
                        severity = ((num6 >= 25) ? 4f : 3f);
                    }
                }
            }
            parent.Severity = severity;
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

public class Comp_NatureConnectionHediff : HediffComp
{
    public override void CompPostTick(ref float severityAdjustment)
    {
        if (!base.Pawn.IsHashIntervalTick(120))
        {
            return;
        }
        int num = CountNearPlants(parent.pawn);
        int num2 = num;
        int num3 = num2;
        float severity;
        if (num3 < 5)
        {
            severity = 0.5f;
        }
        else
        {
            int num4 = num3;
            if (num4 < 10)
            {
                severity = 1f;
            }
            else
            {
                int num5 = num3;
                if (num5 < 15)
                {
                    severity = 2f;
                }
                else
                {
                    int num6 = num3;
                    severity = ((num6 >= 25) ? 4f : 3f);
                }
            }
        }
        parent.Severity = severity;
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