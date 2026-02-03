using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_ToxonStinger : HediffCompProperties
    {
        public float severityPerHourEmpty;

        public float severityPerHourLoaded;

        public HediffCompProperties_ToxonStinger()
        {
            compClass = typeof(HediffComp_ToxonStinger);
        }
    }
}
