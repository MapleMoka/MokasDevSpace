using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_HydroRegen : HediffCompProperties
    {
        public int wetHours = 5;
        //public IntRange rateInTicks;
        public float healAmount = 1f;

        public HediffCompProperties_HydroRegen() => this.compClass = typeof(HediffComp_HydroRegen);
    }
}
