using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_FloralRegeneration : HediffCompProperties
    {
        //public int solarDays = 9;

        public int daysToHeal = 0;
        public int daysToGenerate = 0;
        public int amountToHeal = 5;

        public HediffCompProperties_FloralRegeneration() => this.compClass = typeof(HediffComp_FloralRegeneration);
    }
}
