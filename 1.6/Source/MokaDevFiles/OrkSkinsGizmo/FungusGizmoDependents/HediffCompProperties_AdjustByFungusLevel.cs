using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_AdjustByFungusLevel : HediffCompProperties
    {
        public HediffCompProperties_AdjustByFungusLevel()
        {
            /*
             * this.radiusTier1 = 3f;
            this.radiusTier2 = 5f;
            this.radiusTier3 = 8f;
            this.damageType = (DamageDef)null;
            */
            this.compClass = typeof(HediffComp_AdjustByFungusLevel);
        }
    }
}
