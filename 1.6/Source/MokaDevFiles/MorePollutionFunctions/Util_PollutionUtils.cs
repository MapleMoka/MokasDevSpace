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
        public static bool InToxGas(this Pawn pawn)
        {
            if (pawn.Map == null) return false;
            if (pawn.Position.AnyGas(pawn.Map, GasType.ToxGas))
                return true;
            else return false;
        }
    }
}
