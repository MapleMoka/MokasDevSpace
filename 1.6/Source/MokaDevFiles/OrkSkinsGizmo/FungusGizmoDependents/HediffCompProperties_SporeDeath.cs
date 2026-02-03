using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_SporeDeathGiz : HediffCompProperties
    {
        public int radius;
        public float multiplierOne;
        public float multiplierTwo;
        public float multiplierThree;
        public float multiplierFour;

        public DamageDef damageType;

        public HediffCompProperties_SporeDeathGiz()
        {
            /*
             * this.radiusTier1 = 3f;
            this.radiusTier2 = 5f;
            this.radiusTier3 = 8f;
            this.damageType = (DamageDef)null;
            */
            this.compClass = typeof(HediffComp_SporeDeathGiz);
        }
    }
}
