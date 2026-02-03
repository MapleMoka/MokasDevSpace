using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_PhotoSynthesis : HediffCompProperties
    {
        public float growthRate = 0.00005f;

        public HediffCompProperties_PhotoSynthesis() => this.compClass = typeof(HediffComp_PhotoSynthesis);
    }
}
