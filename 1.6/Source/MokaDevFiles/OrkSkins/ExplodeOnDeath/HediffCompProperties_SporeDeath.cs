using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_SporeDeath : HediffCompProperties
    {
        public float radiusTier1;
        public float radiusTier2;
        public float radiusTier3;
        public DamageDef damageType;

        public HediffCompProperties_SporeDeath()
        {
            /*
             * this.radiusTier1 = 3f;
            this.radiusTier2 = 5f;
            this.radiusTier3 = 8f;
            this.damageType = (DamageDef)null;
            */
            this.compClass = typeof(HediffComp_SporeDeath);
        }
    }
}
