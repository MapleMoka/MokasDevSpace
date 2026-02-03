using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace MokaDevSpace
{

    public class Hediff_ToxonStinger : HediffWithComps
    {
        public override string SeverityLabel
        {
            get
            {
                if (Severity == 0f)
                {
                    return null;
                }
                return Severity.ToStringPercent();
            }
        }
        //public override void PostAdd(DamageInfo? dinfo)
        //{
        //    base.PostAdd(dinfo);
        //}
        public override void PostRemoved()
        {
            base.PostRemoved();
            this.pawn.health.AddHediff(MCM_DefOf.Moka_StingerNumb);

        }
    }
}
