using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_XylemOld : HediffCompProperties
    {
        public float intakeSpeed;
        public float evaporationSpeed;
        public float maxStorage;
        public SimpleCurve injuryHealCurve;
        public SimpleCurve flameDMGCurve;
        public SimpleCurve fertilityCurve;
        public SimpleCurve nutritionCapCurve;

        public HediffCompProperties_XylemOld() => this.compClass = typeof(HediffComp_XylemOld);
    }
}
