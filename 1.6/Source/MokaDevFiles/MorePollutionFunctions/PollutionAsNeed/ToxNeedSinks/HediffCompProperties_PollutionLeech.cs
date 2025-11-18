using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_PollutionLeech : HediffCompProperties
    {
        public HediffCompProperties_PollutionLeech() => this.compClass = typeof(HediffComp_PollutionLeech);
    }
}
