using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Roos_Faun_Xenotype;
using Verse;

namespace MokaDevSpace
{

    public class HediffCompProperties_NearPsykiks : HediffCompProperties
    {
        public float appliedRadius;
        public HediffCompProperties_NearPsykiks()
        {
            compClass = typeof(HediffComp_NearPsykiks);
        }
    }
}