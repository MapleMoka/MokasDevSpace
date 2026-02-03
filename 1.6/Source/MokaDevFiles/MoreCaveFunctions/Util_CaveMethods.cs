using RimWorld;
using RimWorld.Planet;
using System;
using UnityEngine;
using Verse;

namespace MokaDevSpace
{
    public static class Util_CaveMethods
    {
        //public static bool IsInCave(this Pawn pawn)
        //{         
        //    RoofDef roofDef = (pawn.Spawned ? pawn.Position.GetRoof(pawn.Map) : null);

        //    if (roofDef.isNatural && roofDef.isThickRoof)
        //        return true;
        //    else
        //    {
        //        return false;
        //    }
        //}
        public static bool IsInCave(this Pawn pawn) => ((Thing)pawn).Map != null && pawn.Spawned && pawn.Position.GetRoof(pawn.Map).isNatural && pawn.Position.GetRoof(pawn.Map).isThickRoof;

        public static void OffsetDust(Pawn pawn, float offset, bool applyStatFactor = true)
        {
            if (!ModsConfig.BiotechActive)
            {
                return;
            }
            //if (offset > 0f && applyStatFactor)
            //{
            //    offset *= pawn.GetStatValue(MCM_DefOf.Moka_PollutionGainFactor);
            //}
            //Gene_Resource_NaniteDrain gene_NaniteDrain = pawn.genes?.GetFirstGeneOfType<Gene_Resource_NaniteDrain>();
            //if (gene_NaniteDrain != null)
            //{
            //    GeneResourceDrainUtility.OffsetResource(gene_NaniteDrain, offset);
            //    return;
            //}
            Gene_Resource_Dust gene_Dust = pawn.genes?.GetFirstGeneOfType<Gene_Resource_Dust>();
            if (gene_Dust != null)
            {
                gene_Dust.Value += offset;
            }
        }
    }
}
