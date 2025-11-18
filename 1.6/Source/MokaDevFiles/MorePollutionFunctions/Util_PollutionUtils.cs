using RimWorld.Planet;
using RimWorld;
using Verse;

namespace MokaDevSpace
{
    public static class Util_PollutionUtils
    {
        public static bool OnPollutedTile(this Pawn pawn)
        {
            if (pawn.Map == null) return false;
            if (pawn.Map.pollutionGrid.IsPolluted(pawn.Position))
                return true;
            //if (CaravanUtility.IsCaravanMember(pawn) && )
            else return false;
        }
        public static bool HasToxicBuildup(this Pawn pawn, HediffDef hediffToCheck)
        {
            Hediff firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(hediffToCheck);
            if (firstHediffOfDef != null)
                return true;
            else return false;
        }
        public static bool HasToxicBuildup(this Pawn pawn)
        {
            Hediff firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.ToxicBuildup);
            if (firstHediffOfDef != null)
                return true;
            else return false;
        }
        public static bool InToxGas(this Pawn pawn)
        {
            if (pawn.Map == null) return false;
            if (pawn.Position.AnyGas(pawn.Map, GasType.ToxGas))
                return true;
            else return false;
        }
        public static void OffsetPollution(Pawn pawn, float offset, bool applyStatFactor = true)
        {
            if (!ModsConfig.BiotechActive)
            {
                return;
            }
            if (offset > 0f && applyStatFactor)
            {
                offset *= pawn.GetStatValue(StatDefOf.HemogenGainFactor);
            }
            Gene_Resource_PollutionDrain gene_HemogenDrain = pawn.genes?.GetFirstGeneOfType<Gene_Resource_PollutionDrain>();
            if (gene_HemogenDrain != null)
            {
                GeneResourceDrainUtility.OffsetResource(gene_HemogenDrain, offset);
                return;
            }
            Gene_Resource_Pollution gene_Hemogen = pawn.genes?.GetFirstGeneOfType<Gene_Resource_Pollution>();
            if (gene_Hemogen != null)
            {
                gene_Hemogen.Value += offset;
            }
        }
    }
}
