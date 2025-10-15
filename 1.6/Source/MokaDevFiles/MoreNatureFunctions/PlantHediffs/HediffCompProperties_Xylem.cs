using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffCompProperties_Xylem : HediffCompProperties
    {
        public float intakeSpeed;
        public float evaporationSpeed;
        public float maxStorage;
        public SimpleCurve injuryHealCurve;
        public SimpleCurve flameDMGCurve;
        public SimpleCurve fertilityCurve;
        public SimpleCurve nutritionCapCurve;

        public HediffCompProperties_Xylem() => this.compClass = typeof(HediffComp_Xylem);
    }
}
