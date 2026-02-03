using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace MokaDevSpace
{
    internal class ThoughtWorker_PollutionNeed : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn pawn)
        {
            Need_Pollution thisNeed = pawn.needs.TryGetNeed<Need_Pollution>();
            if (thisNeed == null)
                return ThoughtState.Inactive;
            switch (thisNeed.EffectiveMoodShiftPerLevel)
            {
                case Need_Pollution.MoodLevels.Depleted:
                    return ThoughtState.ActiveAtStage(0);
                case Need_Pollution.MoodLevels.Diminished:
                    return ThoughtState.ActiveAtStage(1);
                case Need_Pollution.MoodLevels.Stable:
                    return ThoughtState.ActiveAtStage(2);
                case Need_Pollution.MoodLevels.Inactive:
                    return ThoughtState.Inactive;
                case Need_Pollution.MoodLevels.Refined:
                    return ThoughtState.ActiveAtStage(3);
                case Need_Pollution.MoodLevels.Enriched:
                    return ThoughtState.ActiveAtStage(4);
                case Need_Pollution.MoodLevels.Radiant:
                    return ThoughtState.ActiveAtStage(5);
                default:
                    throw new InvalidOperationException("Unknown MoodBuff level");
            }
        }
    }
}
