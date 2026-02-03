using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    public class ThoughtWorker_IsWet : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn pawn)
        {
            if (!pawn.PawnIsWet())
            {
                return ThoughtState.Inactive;
            }
            return ThoughtState.ActiveDefault;
        }
    }
}
