using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_AffectsPawnsOnly : HediffComp
    {
        public HediffCompProperties_AffectsPawnsOnly Props
        {
            get
            {
                return (HediffCompProperties_AffectsPawnsOnly)this.props;
            }
        }
        public override void CompPostTick(ref float severityAdjustment)
        {
            base.CompPostTick(ref severityAdjustment);
            if (!(this.parent.pawn == Pawn))
            {
                this.parent.pawn.health.RemoveHediff(this.parent);
            }
        }
    }
}
