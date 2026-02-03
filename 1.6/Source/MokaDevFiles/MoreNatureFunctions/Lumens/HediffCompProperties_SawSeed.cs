using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_SawSeed : HediffCompProperties
    {
        public float severityPerHourNotInSun;
        public float severityPerHourInSun;
        public float severityPerHourWithLumen;
        public float lumenDrainPerSeverityOffset;

        public HediffCompProperties_SawSeed()
        {
            compClass = typeof(HediffComp_SawSeed);
        }

    }
}
