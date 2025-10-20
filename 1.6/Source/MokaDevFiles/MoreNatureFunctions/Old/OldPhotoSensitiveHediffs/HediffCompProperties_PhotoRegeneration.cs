using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_PhotoRegeneration : HediffCompProperties
    {
        public int solarDays = 9;

        public HediffCompProperties_PhotoRegeneration() => this.compClass = typeof(HediffComp_PhotoRegeneration);
    }
}
