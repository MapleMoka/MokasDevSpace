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
    //internal class ThoughtWorker_NatureExposureNeed : ThoughtWorker
    //{
    //    protected override ThoughtState CurrentStateInternal(Pawn pawn)
    //    {
    //        Need_NatureExposure thisNeed = pawn.needs.TryGetNeed<Need_NatureExposure>();
    //        if (thisNeed == null)
    //            return ThoughtState.Inactive;
    //        switch (thisNeed.EffectiveMoodShiftPerLevel)
    //        {
    //            case Need_NatureExposure.MoodLevels.Sickly:
    //                return ThoughtState.ActiveAtStage(0);
    //            case Need_NatureExposure.MoodLevels.Autumnal:
    //                return ThoughtState.ActiveAtStage(1);
    //            case Need_NatureExposure.MoodLevels.Foliant:
    //                return ThoughtState.ActiveAtStage(2);
    //            case Need_NatureExposure.MoodLevels.Inactive:
    //                return ThoughtState.Inactive;
    //            case Need_NatureExposure.MoodLevels.Mossy:
    //                return ThoughtState.ActiveAtStage(3);
    //            case Need_NatureExposure.MoodLevels.Lush:
    //                return ThoughtState.ActiveAtStage(4);
    //            case Need_NatureExposure.MoodLevels.Verdant:
    //                return ThoughtState.ActiveAtStage(5);
    //            default:
    //                throw new InvalidOperationException("Unknown MoodBuff level");
    //        }

    //    }
    //}
}
