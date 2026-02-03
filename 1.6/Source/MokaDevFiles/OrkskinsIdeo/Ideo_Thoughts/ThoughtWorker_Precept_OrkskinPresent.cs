using RimWorld;
using Verse;

namespace MokaDevSpace
{
    public class ThoughtWorker_Precept_OrkskinPresent : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p)
        {
            if (!ModsConfig.BiotechActive || !ModsConfig.IdeologyActive || p.IsOrkSkin())
            {
                return ThoughtState.Inactive;
            }
            foreach (Pawn item in p.MapHeld.mapPawns.AllPawnsSpawned)
            {
                if (item.IsOrkSkin() && (item.IsPrisonerOfColony || item.IsSlaveOfColony || item.IsColonist))
                {
                    return ThoughtState.ActiveDefault;
                }
            }
            return ThoughtState.Inactive;
        }
    }
}

