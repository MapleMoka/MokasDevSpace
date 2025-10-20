using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_FungalXenoCheck : HediffCompProperties
    {
        public XenotypeDef xenotype;
        public float changeAtSeverity;

        public HediffCompProperties_FungalXenoCheck()
        {
            this.compClass = typeof(HediffComp_FungalXenoCheck);
        }
    }
}
