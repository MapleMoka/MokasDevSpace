using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_ToxonToPsych : HediffCompProperties
    {
        public float severityPerHourEmpty;

        public float severityPerHourLoaded;

        public HediffCompProperties_ToxonToPsych()
        {
            compClass = typeof(HediffComp_ToxonToPsych);
        }
    }
}
