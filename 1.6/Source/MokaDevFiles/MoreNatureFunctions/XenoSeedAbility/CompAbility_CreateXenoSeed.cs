using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class CompAbilityEffect_CreateXenoSeed : CompAbilityEffect_WithDuration

    {
        public CompAbilityProperties_CreateXenoSeed Props
        {
            get => (CompAbilityProperties_CreateXenoSeed)this.props;
        }
        public override void Apply(LocalTargetInfo target, LocalTargetInfo dest)
        {
            base.Apply(target, dest);
            if (this.Props.ignoreSelf && target.Pawn == this.parent.pawn)
                return;
            if (!this.Props.onlyApplyToSelf && this.Props.applyToTarget)
                this.ApplyFunc(target.Pawn, this.parent.pawn);
            if (!this.Props.applyToSelf && !this.Props.onlyApplyToSelf)
                return;
            this.ApplyFunc(this.parent.pawn, target.Pawn);
        }
        protected void ApplyFunc(Pawn target, Pawn other)
        {
            if (target == null)
                return;
            else
            {
                Hediff hediff = HediffMaker.MakeHediff(this.Props.hediffDef, target, this.Props.onlyBrain ? target.health.hediffSet.GetBrain() : (BodyPartRecord)null);
                HediffComp_Disappears comp1 = hediff.TryGetComp<HediffComp_Disappears>();
                if (comp1 != null)
                    comp1.ticksToDisappear = this.GetDurationSeconds(target).SecondsToTicks();
                if ((double)this.Props.severity >= 0.0)
                    hediff.Severity = this.Props.severity;
                HediffComp_Link comp2 = hediff.TryGetComp<HediffComp_Link>();
                if (comp2 != null)
                {
                    comp2.other = (Thing)other;
                    comp2.drawConnection = target == this.parent.pawn;
                }
                target.health.AddHediff(hediff);

                TrySpawnXenoSeed(target, this.Props.xenoSeed);
            }
        }
        public void TrySpawnXenoSeed(Pawn target, ThingDef xenoSeed)
        {
            ThingWithComps newXenoSeed = (ThingWithComps)ThingMaker.MakeThing(xenoSeed);
            if (GenPlace.TryPlaceThing((Thing)newXenoSeed, target.PositionHeld, target.MapHeld, ThingPlaceMode.Near))
                return;
            Log.Error("Could not drop item near " + target.PositionHeld.ToString());
        }
    }
}
