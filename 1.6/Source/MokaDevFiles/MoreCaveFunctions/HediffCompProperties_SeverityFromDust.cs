using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_SeverityFromDust : HediffCompProperties
    {
        public float severityPerHourEmpty;

        public float severityPerHourLoaded;

        public HediffCompProperties_SeverityFromDust()
        {
            compClass = typeof(HediffComp_SeverityFromDust);
        }
    }
}
