using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_SeverityFromFungus : HediffCompProperties
    {
        public float severityPerHourEmpty;

        public float severityPerHourLoaded;

        public HediffCompProperties_SeverityFromFungus()
        {
            compClass = typeof(HediffComp_SeverityFromFungus);
        }
    }
}
