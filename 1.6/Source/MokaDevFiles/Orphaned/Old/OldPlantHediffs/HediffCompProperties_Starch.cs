using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_Starch : HediffCompProperties
    {
        public float startThreshold;
        //public float conversionRate;
        public float starvationThreshold;
        public float conversionSpeed;
        public float starvationSpeed;
        public float maxStarch;
        public SimpleCurve speedUpCurve;
        public SimpleCurve tirednessReductionCurve;
        public SimpleCurve learningRateCurve;

        public HediffCompProperties_Starch() => this.compClass = typeof(HediffComp_Starch);
    }
}
