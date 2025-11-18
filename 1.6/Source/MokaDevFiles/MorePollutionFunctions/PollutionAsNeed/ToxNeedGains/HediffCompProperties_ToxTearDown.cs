using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_ToxTearDown : HediffCompProperties
    {
        public HediffCompProperties_ToxTearDown() => this.compClass = typeof(HediffComp_ToxTearDown);
    }
}
