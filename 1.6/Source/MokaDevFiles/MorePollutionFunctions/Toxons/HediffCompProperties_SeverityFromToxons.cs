using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_SeverityFromToxons : HediffCompProperties
    {
        public float severityPerHourEmpty;

        public float severityPerHourLoaded;

        public HediffCompProperties_SeverityFromToxons()
        {
            compClass = typeof(HediffComp_SeverityFromToxons);
        }
    }
}
