using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse.AI;
using Verse;

namespace MFM
{
    internal class JobDriver_TendToBeeHive : JobDriver
    {
        protected Building_BeeHive Apiary => (Building_BeeHive)this.job.GetTarget(TargetIndex.A).Thing;

        public override bool TryMakePreToilReservations(bool errorOnFailed) => ReservationUtility.Reserve(this.pawn, (LocalTargetInfo)(Thing)this.Apiary, this.job, 1, -1, (ReservationLayerDef)null, errorOnFailed);

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden<JobDriver_TendToBeeHive>(TargetIndex.A);
            this.FailOnBurningImmobile<JobDriver_TendToBeeHive>(TargetIndex.A);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
            yield return Toils_General.Wait(500).FailOnDestroyedNullOrForbidden<Toil>(TargetIndex.A).FailOnCannotTouch<Toil>(TargetIndex.A, PathEndMode.Touch).FailOn<Toil>((Func<bool>)(() => !this.Apiary.needTend)).WithProgressBarToilDelay(TargetIndex.A);
            yield return new Toil()
            {
                initAction = (Action)(() =>
                {
                    if (Rand.RangeInclusive(0, 11 - this.pawn.skills.skills.Find((Predicate<SkillRecord>)(r => r.def.defName == "Animals")).levelInt / 2) <= 5)
                    {
                        this.Apiary.tickBeforeTend += 120000;
                    }
                    else
                    {
                        this.pawn.needs.mood.thoughts.memories.TryGainMemoryFast(MFM_DefOf.Moka_BeeStingTht);
                        MoteMaker.ThrowText(this.pawn.DrawPos, this.pawn.Map, "Tending failed", 5f);
                        this.pawn.jobs.StartJob(new Job(MFM_DefOf.Moka_TendBeeHive, this.TargetA), JobCondition.None, (ThinkNode)null, false, true, (ThinkTreeDef)null, new JobTag?(), false, false, new bool?(), false, true);
                    }
                    this.pawn.skills.skills.Find((Predicate<SkillRecord>)(r => r.def.defName == "Animals")).Learn(20f, false);
                }),
                defaultCompleteMode = ToilCompleteMode.Instant
            };
        }
    }
}
