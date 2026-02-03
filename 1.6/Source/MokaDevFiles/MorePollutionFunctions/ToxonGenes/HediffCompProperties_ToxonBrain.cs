using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_ToxonBrain : HediffCompProperties
    {
        public float severityPerHourEmpty;

        public float severityPerHourLoaded;

        public HediffCompProperties_ToxonBrain()
        {
            compClass = typeof(HediffComp_ToxonBrain);
        }
    }
}
