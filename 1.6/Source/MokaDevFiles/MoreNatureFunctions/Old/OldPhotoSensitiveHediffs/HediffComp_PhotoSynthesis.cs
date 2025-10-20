using RimWorld;
using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_PhotoSynthesis : HediffComp
    {

        public HediffCompProperties_PhotoSynthesis Props => (HediffCompProperties_PhotoSynthesis)this.props;

        public override void CompExposeData()
        {
            base.CompExposeData();
            Scribe_Values.Look<float>(ref this.Props.growthRate, "growthRate");
        }

        public override void CompPostTick(ref float severityAdjustment)
        {
            Pawn pawn = this.parent.pawn;

            if (!pawn.Spawned)
                return;

            if (pawn.PawnInLight())
            {

                float foodDiff = this.Props.growthRate;

                if (((Thing)pawn).Map != null)
                    foodDiff *= ((Thing)pawn).Map.skyManager.CurSkyGlow;

                Need_Food food = pawn.needs.food;
                ((Need)food).CurLevel = ((Need)food).CurLevel + foodDiff;
            }
        }
    }
}
