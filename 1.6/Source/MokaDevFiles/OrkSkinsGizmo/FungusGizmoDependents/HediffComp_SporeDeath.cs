using MokaDevSpace;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_SporeDeathGiz : HediffComp
    {
        public HediffCompProperties_SporeDeathGiz Props
        {
            get
            {
                return (HediffCompProperties_SporeDeathGiz)this.props;
            }
        }

        private Gene_Resource_Fungus cachedFungusGene;

        private Gene_Resource_Fungus localFungusGene => cachedFungusGene ?? (cachedFungusGene = base.Pawn.genes.GetFirstGeneOfType<Gene_Resource_Fungus>());

        public override void Notify_PawnDied(DamageInfo? dinfo, Hediff culprit = null)
        {
            Corpse corpse = this.parent.pawn.Corpse;
            if (this.Props.damageType == null)
                this.Props.damageType = DamageDefOf.Flame;
            if (corpse == null)
                return;

            if (localFungusGene.Value < 0.25)
            {
                this.LoadExplosion(corpse, (this.Props.radius * this.Props.multiplierOne));
                return;
            }
            else if (localFungusGene.Value < 0.5)
            {
                this.LoadExplosion(corpse, (this.Props.radius * this.Props.multiplierTwo));
                return;
            }
            else if (localFungusGene.Value < 0.75)
            {
                this.LoadExplosion(corpse, (this.Props.radius * this.Props.multiplierThree));
                return;
            }
            else
            {
                this.LoadExplosion(corpse, (this.Props.radius * this.Props.multiplierFour));
                return;
            }
        }

        private void LoadExplosion(Corpse corpse, float radius)
        {
            GenExplosion.DoExplosion(corpse.Position, corpse.Map, radius, this.Props.damageType, (Thing)corpse.InnerPawn, -1, -1f, (SoundDef)null, (ThingDef)null, (ThingDef)null, (Thing)null, (ThingDef)null, 0.0f, 1, new GasType?(), new float?(), 0, false, (ThingDef)null, 0.0f, 0, 0.0f, false, new float?(), (List<Thing>)null, new FloatRange?(), true, 1f, 0.0f, true, (ThingDef)null, 1f, (SimpleCurve)null, (List<IntVec3>)null, (ThingDef)null, (ThingDef)null);

        }

        public HediffComp_SporeDeathGiz()
        {
        }
    }
}
