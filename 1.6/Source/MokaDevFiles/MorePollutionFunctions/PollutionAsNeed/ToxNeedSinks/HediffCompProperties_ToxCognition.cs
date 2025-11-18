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
        public HediffCompProperties_ToxCognition() => this.compClass = typeof(HediffComp_ToxCognition);
    }
}
