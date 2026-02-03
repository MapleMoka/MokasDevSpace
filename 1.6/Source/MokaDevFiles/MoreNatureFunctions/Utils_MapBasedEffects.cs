using RimWorld.Planet;
using RimWorld;
using Verse;

namespace MokaDevSpace
{
    public static class Utils_MapBasedEffects
    {
        public static bool PawnInLight(this Pawn pawn) => ((Thing)pawn).Map != null && pawn.Spawned && (double)pawn.Map.glowGrid.GroundGlowAt(pawn.Position, false) >= 0.51 || CaravanUtility.IsCaravanMember(pawn) && GenLocalDate.HourInteger((Thing)pawn) >= 6 && GenLocalDate.HourInteger((Thing)pawn) <= 20;

        public static bool PawnIsWet(this Pawn pawn)
        {
            return OnWater(pawn) || InRain(pawn) || InRiver(pawn);
        }

        public static bool InRain(this Pawn pawn)
        {
            return pawn.Map != null && pawn.Position.GetTerrain(pawn.Map) != null &&
                   !pawn.Position.Roofed(pawn.Map) && pawn.Map.weatherManager.RainRate > 0.01f;
        }

        public static bool OnWater(this Pawn pawn)
        {
            return pawn.Map != null && pawn.Position.GetTerrain(pawn.Map) != null && pawn.Position.GetTerrain(pawn.Map).IsWater;
        }

        public static bool InRiver(this Pawn pawn)
        {
            return pawn.Map != null && pawn.Position.GetTerrain(pawn.Map) != null && pawn.Position.GetTerrain(pawn.Map).IsRiver;
        }
        public static void OffsetLumens(Pawn pawn, float offset, bool applyStatFactor = true)
        {
            if (!ModsConfig.BiotechActive)
            {
                return;
            }
            //if (offset > 0f && applyStatFactor)
            //{
            //    offset *= pawn.GetStatValue(MCM_DefOf.Moka_PollutionGainFactor);
            //}
            Gene_Resource_LumenDrain gene_LumenDrain = pawn.genes?.GetFirstGeneOfType<Gene_Resource_LumenDrain>();
            if (gene_LumenDrain != null)
            {
                GeneResourceDrainUtility.OffsetResource(gene_LumenDrain, offset);
                return;
            }
            Gene_Resource_Lumens gene_Lumen = pawn.genes?.GetFirstGeneOfType<Gene_Resource_Lumens>();
            if (gene_Lumen != null)
            {
                gene_Lumen.Value += offset;
            }
        }

    }
}