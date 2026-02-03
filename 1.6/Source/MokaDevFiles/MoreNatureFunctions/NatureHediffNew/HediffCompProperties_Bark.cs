using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_Bark : HediffCompProperties
    {
        public float maxAge;
        public SimpleCurve slowdownCurve;
        public SimpleCurve bluntProtectionCurve;
        public SimpleCurve staggerStunCurve;

        public HediffCompProperties_Bark() => this.compClass = typeof(HediffComp_Bark);
    }
}
