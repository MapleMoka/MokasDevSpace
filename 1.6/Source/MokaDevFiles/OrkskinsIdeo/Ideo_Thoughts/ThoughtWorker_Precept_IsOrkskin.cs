using RimWorld;
using Verse;

namespace MokaDevSpace
{
    public class ThoughtWorker_Precept_IsOrkskin : ThoughtWorker_Precept
    {
        protected override ThoughtState ShouldHaveThought(Pawn p)
        {
            if (!ModsConfig.BiotechActive || !ModsConfig.IdeologyActive)
            {
                return ThoughtState.Inactive;
            }
            return p.IsOrkSkin();
        }
    }
}
