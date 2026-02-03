using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
//using VanillaRacesExpandedPhytokin;
using Verse;
using Verse.AI;

namespace MokaDevSpace
{

    public class JobDriver_ConsumeWastepack : JobDriver
    {
        private const int baseConsumeTicks = 500;

        public Thing ToConsume => job.GetTarget(TargetIndex.A).Thing;

        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return pawn.Reserve(ToConsume, job, 1, job.count);
        }

        protected override IEnumerable<Toil> MakeNewToils()
        {
            Log.Message("Job is entered");
            Toil chew = ToilMaker.MakeToil("ConsumeWastepack");
            chew.initAction = delegate
            {
                Thing toConsume = ToConsume;
                Pawn pawn = base.pawn;
                pawn.jobs.curDriver.ticksLeftThisToil = Mathf.RoundToInt(500f * durationMultiplier(pawn));
                if (toConsume.Spawned)
                {
                    toConsume.Map.physicalInteractionReservationManager.Reserve(pawn, pawn.CurJob, toConsume);
                }
            };
            chew.tickAction = delegate
            {
                Thing toConsume = ToConsume;
                if (toConsume.Spawned)
                {
                    chew.actor.rotationTracker.FaceCell(toConsume.Position);
                }
                if (chew.actor.CurJob.GetTarget(TargetIndex.B).IsValid)
                {
                    chew.actor.rotationTracker.FaceCell(chew.actor.CurJob.GetTarget(TargetIndex.B).Cell);
                }
                chew.actor.GainComfortFromCellIfPossible(1);
            };
            chew.WithProgressBar(TargetIndex.A, delegate
            {
                Thing toConsume = ToConsume;
                return (toConsume == null) ? 1f : (1f - (float)chew.actor.jobs.curDriver.ticksLeftThisToil / Mathf.Round(500f * durationMultiplier(chew.actor)));
            });
            chew.defaultCompleteMode = ToilCompleteMode.Delay;
            chew.handlingFacing = true;
            chew.FailOnDestroyedOrNull(TargetIndex.A);
            chew.AddFinishAction(delegate
            {
                Pawn actor = chew.actor;
                if (actor.CurJob != null)
                {
                    Thing toConsume = ToConsume;
                    if (toConsume != null && actor.Map.physicalInteractionReservationManager.IsReservedBy(actor, toConsume))
                    {
                        actor.Map.physicalInteractionReservationManager.Release(actor, actor.CurJob, toConsume);
                    }
                }
            });
            chew.WithEffect(MCM_DefOf.EatVegetarian, TargetIndex.A);
            chew.PlaySustainerOrSound(MCM_DefOf.Meal_Eat);
            yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.ClosestTouch);
            yield return Toils_Ingest.PickupIngestible(TargetIndex.A, pawn);
            yield return CarryIngestibleToChewSpot(pawn, TargetIndex.A);
            yield return Toils_Ingest.FindAdjacentEatSurface(TargetIndex.B, TargetIndex.A);
            yield return chew;
            Toil finalize = ToilMaker.MakeToil("AteMetal");
            finalize.initAction = delegate
            {
                Pawn actor = finalize.actor;
                Thing toConsume = ToConsume;
                if (actor.needs.mood != null)
                {
                    if (!(actor.Position + actor.Rotation.FacingCell).HasEatSurface(actor.Map) && actor.GetPosture() == PawnPosture.Standing)
                    {
                        actor.needs.mood.thoughts.memories.TryGainMemory(ThoughtDefOf.AteWithoutTable);
                    }
                    Room room = actor.GetRoom();
                    if (room != null)
                    {
                        int scoreStageIndex = RoomStatDefOf.Impressiveness.GetScoreStageIndex(room.GetStat(RoomStatDefOf.Impressiveness));
                        if (ThoughtDefOf.AteInImpressiveDiningRoom.stages[scoreStageIndex] != null)
                        {
                            actor.needs.mood.thoughts.memories.TryGainMemory(ThoughtMaker.MakeThought(ThoughtDefOf.AteInImpressiveDiningRoom, scoreStageIndex));
                        }
                    }
                }
                Gene_Resource_ToxonFeeding firstGeneOfType = actor.genes.GetFirstGeneOfType<Gene_Resource_ToxonFeeding>();
                //firstGeneOfType.Notify_IngestedThing(toConsume, 1);
                firstGeneOfType.ValueChange(0.05f);
                toConsume.Destroy();
            };
            finalize.defaultCompleteMode = ToilCompleteMode.Instant;
            yield return finalize;
        }

        public static Toil ConsumeWastepack(Pawn pawn, float durationMulti, TargetIndex ingestibleInd)
        {
            Toil chew = ToilMaker.MakeToil("ConsumeWastepack");
            Thing ToConsume = chew.actor.CurJob.GetTarget(ingestibleInd).Thing;
            chew.initAction = delegate
            {
                Thing thing = ToConsume;
                Pawn actor = chew.actor;
                actor.jobs.curDriver.ticksLeftThisToil = Mathf.RoundToInt(500f * durationMulti);
                if (thing.Spawned)
                {
                    thing.Map.physicalInteractionReservationManager.Reserve(pawn, actor.CurJob, thing);
                }
            };
            chew.tickAction = delegate
            {
                Thing thing = ToConsume;
                if (pawn != chew.actor)
                {
                    chew.actor.rotationTracker.FaceCell(pawn.Position);
                }
                if (thing.Spawned)
                {
                    chew.actor.rotationTracker.FaceCell(thing.Position);
                }
                if (chew.actor.CurJob.GetTarget(TargetIndex.B).IsValid)
                {
                    chew.actor.rotationTracker.FaceCell(chew.actor.CurJob.GetTarget(TargetIndex.B).Cell);
                }
                chew.actor.GainComfortFromCellIfPossible(1);
            };
            chew.WithProgressBar(TargetIndex.A, delegate
            {
                Thing thing = ToConsume;
                return (thing == null) ? 1f : (1f - (float)chew.actor.jobs.curDriver.ticksLeftThisToil / Mathf.Round(500f * durationMulti));
            });
            chew.defaultCompleteMode = ToilCompleteMode.Delay;
            chew.handlingFacing = true;
            chew.FailOnDestroyedOrNull(TargetIndex.A);
            chew.AddFinishAction(delegate
            {
                Pawn pawn2 = pawn;
                if (pawn2.CurJob != null)
                {
                    Thing thing = ToConsume;
                    if (thing != null && pawn2.Map.physicalInteractionReservationManager.IsReservedBy(pawn2, thing))
                    {
                        pawn2.Map.physicalInteractionReservationManager.Release(pawn2, pawn2.CurJob, thing);
                    }
                }
            });
            chew.WithEffect(MCM_DefOf.EatVegetarian, TargetIndex.A);
            chew.PlaySustainerOrSound(MCM_DefOf.Meal_Eat);
            return chew;
        }

        public override bool ModifyCarriedThingDrawPos(ref Vector3 drawPos, ref bool behind)
        {
            //IL_00a9: Unknown result type (might be due to invalid IL or missing references)
            //IL_00ae: Unknown result type (might be due to invalid IL or missing references)
            IntVec3 cell = job.GetTarget(TargetIndex.B).Cell;
            if (pawn.pather.Moving)
            {
                return false;
            }
            if (pawn.carryTracker.CarriedThing == null)
            {
                return false;
            }
            if (cell.IsValid && cell.AdjacentToCardinal(pawn.Position) && cell.HasEatSurface(pawn.Map))
            {
                drawPos = new Vector3((float)cell.x + 0.5f, drawPos.y, (float)cell.z + 0.5f);
                return true;
            }
            return base.ModifyCarriedThingDrawPos(ref drawPos, ref behind);
        }

        private Toil CarryIngestibleToChewSpot(Pawn pawn, TargetIndex ingestibleInd)
        {
            Toil toil = ToilMaker.MakeToil("CarryIngestibleToChewSpot");
            toil.initAction = delegate
            {
                Pawn actor = toil.actor;
                IntVec3 cell = IntVec3.Invalid;
                Thing thing = null;
                Thing thing2 = actor.CurJob.GetTarget(ingestibleInd).Thing;
                Predicate<Thing> baseChairValidator = delegate (Thing t)
                {
                    if (t.def.building == null || !t.def.building.isSittable)
                    {
                        return false;
                    }
                    if (!Toils_Ingest.TryFindFreeSittingSpotOnThing(t, actor, out var cell2))
                    {
                        return false;
                    }
                    if (t.IsForbidden(pawn))
                    {
                        return false;
                    }
                    if (!actor.CanReserve(t))
                    {
                        return false;
                    }
                    if (!t.IsSociallyProper(actor))
                    {
                        return false;
                    }
                    if (t.IsBurning())
                    {
                        return false;
                    }
                    if (t.HostileTo(pawn))
                    {
                        return false;
                    }
                    bool result = false;
                    for (int i = 0; i < 4; i++)
                    {
                        Building edifice = (cell2 + GenAdj.CardinalDirections[i]).GetEdifice(t.Map);
                        if (edifice != null && edifice.def.surfaceType == SurfaceType.Eat)
                        {
                            result = true;
                            break;
                        }
                    }
                    return result;
                };
                thing = GenClosest.ClosestThingReachable(actor.Position, actor.Map, ThingRequest.ForGroup(ThingRequestGroup.BuildingArtificial), PathEndMode.OnCell, TraverseParms.For(actor), 32f, (Thing t) => baseChairValidator(t) && t.Position.GetDangerFor(pawn, t.Map) == Danger.None);
                if (thing == null)
                {
                    cell = RCellFinder.SpotToChewStandingNear(actor, actor.CurJob.GetTarget(ingestibleInd).Thing, (IntVec3 c) => actor.CanReserveSittableOrSpot(c));
                    Danger chewSpotDanger = cell.GetDangerFor(pawn, actor.Map);
                    if (chewSpotDanger != Danger.None)
                    {
                        thing = GenClosest.ClosestThingReachable(actor.Position, actor.Map, ThingRequest.ForGroup(ThingRequestGroup.BuildingArtificial), PathEndMode.OnCell, TraverseParms.For(actor), 32f, (Thing t) => baseChairValidator(t) && (int)t.Position.GetDangerFor(pawn, t.Map) <= (int)chewSpotDanger);
                    }
                }
                if (thing != null && !Toils_Ingest.TryFindFreeSittingSpotOnThing(thing, actor, out cell))
                {
                    Log.Error("Could not find sitting spot on chewing chair! This is not supposed to happen - we looked for a free spot in a previous check!");
                }
                actor.ReserveSittableOrSpot(cell, actor.CurJob);
                actor.Map.pawnDestinationReservationManager.Reserve(actor, actor.CurJob, cell);
                actor.pather.StartPath(cell, PathEndMode.OnCell);
            };
            toil.defaultCompleteMode = ToilCompleteMode.PatherArrival;
            return toil;
        }

        private float durationMultiplier(Pawn pawn)
        {
            return 1f / pawn.GetStatValue(StatDefOf.EatingSpeed);
        }
    }

}
