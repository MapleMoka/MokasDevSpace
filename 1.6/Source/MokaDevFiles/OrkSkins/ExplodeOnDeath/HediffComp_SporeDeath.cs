using RimWorld;
using System.Collections.Generic;
using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_SporeDeath : HediffComp
    {
        public HediffCompProperties_SporeDeath Props
        {
            get
            {
                return (HediffCompProperties_SporeDeath)this.props;
            }
        }

        public override void Notify_PawnDied(DamageInfo? dinfo, Hediff culprit = null)
        {
            Corpse corpse = this.parent.pawn.Corpse;
            if (this.Props.damageType == null)
                this.Props.damageType = DamageDefOf.Flame;
            if (corpse == null)
                return;

            if (this.parent.pawn.genes.Xenotype == MCM_DefOf.Moka_OrkSkinNovice)
            {
                GenExplosion.DoExplosion(corpse.Position, corpse.Map, this.Props.radiusTier1, this.Props.damageType, (Thing)corpse.InnerPawn, -1, -1f, (SoundDef)null, (ThingDef)null, (ThingDef)null, (Thing)null, (ThingDef)null, 0.0f, 1, new GasType?(), new float?(), 0, false, (ThingDef)null, 0.0f, 0, 0.0f, false, new float?(), (List<Thing>)null, new FloatRange?(), true, 1f, 0.0f, true, (ThingDef)null, 1f, (SimpleCurve)null, (List<IntVec3>)null, (ThingDef)null, (ThingDef)null);
            }
            if (this.parent.pawn.genes.Xenotype == MCM_DefOf.Moka_OrkSkinAdept)
            {
                GenExplosion.DoExplosion(corpse.Position, corpse.Map, this.Props.radiusTier2, this.Props.damageType, (Thing)corpse.InnerPawn, -1, -1f, (SoundDef)null, (ThingDef)null, (ThingDef)null, (Thing)null, (ThingDef)null, 0.0f, 1, new GasType?(), new float?(), 0, false, (ThingDef)null, 0.0f, 0, 0.0f, false, new float?(), (List<Thing>)null, new FloatRange?(), true, 1f, 0.0f, true, (ThingDef)null, 1f, (SimpleCurve)null, (List<IntVec3>)null, (ThingDef)null, (ThingDef)null);
            }
            if (this.parent.pawn.genes.Xenotype == MCM_DefOf.Moka_OrkSkinLord)
            {
                GenExplosion.DoExplosion(corpse.Position, corpse.Map, this.Props.radiusTier3, this.Props.damageType, (Thing)corpse.InnerPawn, -1, -1f, (SoundDef)null, (ThingDef)null, (ThingDef)null, (Thing)null, (ThingDef)null, 0.0f, 1, new GasType?(), new float?(), 0, false, (ThingDef)null, 0.0f, 0, 0.0f, false, new float?(), (List<Thing>)null, new FloatRange?(), true, 1f, 0.0f, true, (ThingDef)null, 1f, (SimpleCurve)null, (List<IntVec3>)null, (ThingDef)null, (ThingDef)null);
            }

            //GenExplosion.DoExplosion(corpse.Position, corpse.Map, this.Props.radius, this.Props.damageType, (Thing)corpse.InnerPawn, -1, -1f, (SoundDef)null, (ThingDef)null, (ThingDef)null, (Thing)null, (ThingDef)null, 0.0f, 1, new GasType?(), new float?(), 0, false, (ThingDef)null, 0.0f, 0, 0.0f, false, new float?(), (List<Thing>)null, new FloatRange?(), true, 1f, 0.0f, true, (ThingDef)null, 1f, (SimpleCurve)null, (List<IntVec3>)null, (ThingDef)null, (ThingDef)null);
        }

        public HediffComp_SporeDeath()
        {
        }
    }
}
