using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_AffectsPawnsOnly : HediffCompProperties
    {
        public HediffCompProperties_AffectsPawnsOnly()
        {
            this.compClass = typeof(HediffComp_AffectsPawnsOnly);
        }
    }
}
