using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class Gene_FungalChild : Gene
    {
        public override void PostAdd()
        {
            base.PostAdd();

            Ext_FungalChildren modExtension = def.GetModExtension<Ext_FungalChildren>();
            if (this.pawn.genes.Xenotype == MCM_DefOf.Moka_OrkSkinNovice)
            {
                if (!this.pawn.health.hediffSet.HasHediff(MCM_DefOf.Moka_FungalJoy))
                    pawn.health.AddHediff(MCM_DefOf.Moka_FungalJoy);
            }
            if (this.pawn.genes.Xenotype == MCM_DefOf.Moka_OrkSkinAdept)
            {
                if (!this.pawn.health.hediffSet.HasHediff(MCM_DefOf.Moka_FungalSong))
                    pawn.health.AddHediff(MCM_DefOf.Moka_FungalSong);
            }
            if (this.pawn.genes.Xenotype == MCM_DefOf.Moka_OrkSkinLord)
            {
                if (!this.pawn.health.hediffSet.HasHediff(MCM_DefOf.Moka_FungalChorus))
                    pawn.health.AddHediff(MCM_DefOf.Moka_FungalChorus);
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            Ext_FungalChildren modExtension = def.GetModExtension<Ext_FungalChildren>();
            if (this.pawn.genes.Xenotype == MCM_DefOf.Moka_OrkSkinNovice)
            {
                pawn.health.RemoveHediff(this.pawn.health.hediffSet.GetFirstHediffOfDef(MCM_DefOf.Moka_FungalJoy));
            }
            if (this.pawn.genes.Xenotype == MCM_DefOf.Moka_OrkSkinAdept)
            {
                pawn.health.RemoveHediff(this.pawn.health.hediffSet.GetFirstHediffOfDef(MCM_DefOf.Moka_FungalSong));
            }
            if (this.pawn.genes.Xenotype == MCM_DefOf.Moka_OrkSkinLord)
            {
                pawn.health.RemoveHediff(this.pawn.health.hediffSet.GetFirstHediffOfDef(MCM_DefOf.Moka_FungalChorus));
            }
        }
    }
}
