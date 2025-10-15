using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_ToxCognition : HediffCompProperties
    {
        public float startThreshold;
        //public float conversionRate;
        public float starvationThreshold;
        public float conversionSpeed;
        public float starvationSpeed;
        public float maxStarch;
        public SimpleCurve slowdownCurve;
        public SimpleCurve bluntProtectionCurve;
        public SimpleCurve meatCurve;

        public HediffCompProperties_ToxCognition() => this.compClass = typeof(HediffComp_ToxCognition);
    }
}
