using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_Blubber : HediffCompProperties
    {
        public float startThreshold;
        public float conversionRate;
        public float starvationThreshold;
        public float conversionSpeed;
        public float starvationSpeed;
        public float maxBlubber;
        public SimpleCurve speedUpCurve;
        public SimpleCurve tirednessReductionCurve;
        public SimpleCurve learningRateCurve;

        public HediffCompProperties_Blubber() => this.compClass = typeof(HediffComp_Blubber);
    }
}
