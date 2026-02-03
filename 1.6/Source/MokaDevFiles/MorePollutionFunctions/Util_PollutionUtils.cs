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
        public static void OffsetToxons(Pawn pawn, float offset, bool applyStatFactor = true)
        {
            if (!ModsConfig.BiotechActive)
            {
                return;
            }
            if (offset > 0f && applyStatFactor)
            {
                offset *= pawn.GetStatValue(MCM_DefOf.Moka_PollutionGainFactor);
            }
            Gene_Resource_ToxonDrain gene_ToxonDrain = pawn.genes?.GetFirstGeneOfType<Gene_Resource_ToxonDrain>();
            if (gene_ToxonDrain != null)
            {
                GeneResourceDrainUtility.OffsetResource(gene_ToxonDrain, offset);
                return;
            }
            Gene_Resource_Toxon gene_Toxon = pawn.genes?.GetFirstGeneOfType<Gene_Resource_Toxon>();
            if (gene_Toxon != null)
            {
                gene_Toxon.Value += offset;
            }
            else
            {
                if (pawn.health.hediffSet.HasHediff(HediffDefOf.ToxicBuildup))
                {
                    pawn.health.hediffSet.GetFirstHediffOfDef(HediffDefOf.ToxicBuildup).Severity += 0.02f;
                }
                else
                {
                    pawn.health.AddHediff(HediffDefOf.ToxicBuildup);
                }
            }
        }
    }
}
