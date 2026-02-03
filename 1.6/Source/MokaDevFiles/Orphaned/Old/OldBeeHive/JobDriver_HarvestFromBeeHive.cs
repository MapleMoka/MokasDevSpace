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
    internal class JobDriver_HarvestFromBeeHive : JobDriver
    {
        private const TargetIndex BarrelInd = TargetIndex.A;
        private const TargetIndex BeerToHaulInd = TargetIndex.B;
        private const TargetIndex StorageCellInd = TargetIndex.C;
        private const int Duration = 200;

        protected Building_BeeHive Apiary => (Building_BeeHive)this.job.GetTarget(TargetIndex.A).Thing;

        protected Thing Honey => this.job.GetTarget(TargetIndex.B).Thing;

        public override bool TryMakePreToilReservations(bool errorOnFailed) => ReservationUtility.Reserve(this.pawn, (LocalTargetInfo)(Thing)this.Apiary, this.job, 1, -1, (ReservationLayerDef)null, errorOnFailed);

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedNullOrForbidden<JobDriver_HarvestFromBeeHive>(TargetIndex.A);
            this.FailOnBurningImmobile<JobDriver_HarvestFromBeeHive>(TargetIndex.A);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
            yield return Toils_General.Wait(200).FailOnDestroyedNullOrForbidden<Toil>(TargetIndex.A).FailOnCannotTouch<Toil>(TargetIndex.A, PathEndMode.Touch).FailOn<Toil>((Func<bool>)(() => !this.Apiary.HoneyReady)).WithProgressBarToilDelay(TargetIndex.A);
            yield return new Toil()
            {
                initAction = (Action)(() =>
                {
                    Thing outHoney = this.Apiary.TakeOutHoney();
                    GenPlace.TryPlaceThing(outHoney, this.pawn.Position, this.Map, ThingPlaceMode.Near);
                    StoragePriority currentPriority = StoreUtility.CurrentStoragePriorityOf(outHoney);
                    IntVec3 foundCell;
                    if (StoreUtility.TryFindBestBetterStoreCellFor(outHoney, this.pawn, this.Map, currentPriority, this.pawn.Faction, out foundCell))
                    {
                        this.job.SetTarget(TargetIndex.C, (LocalTargetInfo)foundCell);
                        this.job.SetTarget(TargetIndex.B, (LocalTargetInfo)outHoney);
                        this.job.count = outHoney.stackCount;
                    }
                    else
                        this.EndJobWith(JobCondition.Ongoing | JobCondition.Succeeded);
                }),
                defaultCompleteMode = ToilCompleteMode.Instant
            };
            yield return Toils_Reserve.Reserve(TargetIndex.B, 1, -1, (ReservationLayerDef)null);
            yield return Toils_Reserve.Reserve(TargetIndex.C, 1, -1, (ReservationLayerDef)null);
            yield return Toils_Goto.GotoThing(TargetIndex.B, PathEndMode.ClosestTouch);
            yield return Toils_Haul.StartCarryThing(TargetIndex.B, false, false, false, true);
            Toil carryToCell = Toils_Haul.CarryHauledThingToCell(TargetIndex.C);
            yield return carryToCell;
            yield return Toils_Haul.PlaceHauledThingInCell(TargetIndex.C, carryToCell, true);
        }
    }
}
