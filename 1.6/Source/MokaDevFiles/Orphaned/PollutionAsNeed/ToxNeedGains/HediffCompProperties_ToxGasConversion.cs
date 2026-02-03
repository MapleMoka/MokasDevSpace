using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_ToxGasConversion : HediffCompProperties
    {
        public HediffCompProperties_ToxGasConversion() => this.compClass = typeof(HediffComp_ToxGasConversion);
    }
}
