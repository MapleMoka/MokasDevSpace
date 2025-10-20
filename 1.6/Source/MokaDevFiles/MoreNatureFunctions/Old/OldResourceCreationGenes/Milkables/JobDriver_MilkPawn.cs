using RimWorld;
using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;

namespace MFM
{
    public class JobDriver_MilkPawn : JobDriver_GatherAnimalBodyResources
    {
        private float gatherProgress;

        protected override float WorkTotal => 400f;

        protected override CompHasGatherableBodyResource GetComp(Pawn animal) => (CompHasGatherableBodyResource)animal.TryGetComp<CompMilkableGene>();

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden<JobDriver_MilkPawn>(TargetIndex.A);
            this.FailOnDowned<JobDriver_MilkPawn>(TargetIndex.A);
            this.FailOnNotCasualInterruptible<JobDriver_MilkPawn>(TargetIndex.A);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
            Toil wait = ToilMaker.MakeToil(nameof(MakeNewToils));
            wait.initAction = (Action)(() =>
            {
                Pawn actor = wait.actor;
                Pawn thing = (Pawn)this.job.GetTarget(TargetIndex.A).Thing;
                actor.pather.StopDead();
                if (actor == thing)
                    return;
                PawnUtility.ForceWait(thing, 15000, maintainPosture: true);
            });
            wait.tickAction = (Action)(() =>
            {
                Pawn actor = wait.actor;
                actor.skills.Learn(SkillDefOf.Animals, 0.13f);
                this.gatherProgress += actor.GetStatValue(StatDefOf.AnimalGatherSpeed);
                if ((double)this.gatherProgress < (double)this.WorkTotal)
                    return;
                this.GetComp((Pawn)(Thing)this.job.GetTarget(TargetIndex.A)).Gathered(this.pawn);
                actor.jobs.EndCurrentJob(JobCondition.Succeeded);
            });
            wait.AddFinishAction((Action)(() =>
            {
                Pawn thing = (Pawn)this.job.GetTarget(TargetIndex.A).Thing;
                if (thing == null || thing.CurJobDef != JobDefOf.Wait_MaintainPosture)
                    return;
                thing.jobs.EndCurrentJob(JobCondition.InterruptForced);
            }));
            wait.FailOnDespawnedOrNull<Toil>(TargetIndex.A);
            wait.FailOnCannotTouch<Toil>(TargetIndex.A, PathEndMode.Touch);
            wait.AddEndCondition((Func<JobCondition>)(() => !this.GetComp((Pawn)(Thing)this.job.GetTarget(TargetIndex.A)).ActiveAndFull ? JobCondition.Incompletable : JobCondition.Ongoing));
            wait.defaultCompleteMode = ToilCompleteMode.Never;
            wait.WithProgressBar(TargetIndex.A, (Func<float>)(() => this.gatherProgress / this.WorkTotal));
            wait.activeSkill = (Func<SkillDef>)(() => SkillDefOf.Animals);
            yield return wait;
        }
    }
}

