using Verse;

namespace MokaDevSpace
{
    internal class Gene_FungalChild : Gene
    {
        public override void PostAdd()
        {
            base.PostAdd();

            Ext_FungalChildren modExtension = def.GetModExtension<Ext_FungalChildren>();
            if (this.pawn.genes.Xenotype == modExtension.stage1Xeno)
            {
                if (!this.pawn.health.hediffSet.HasHediff(modExtension.stage1Hediff))
                    pawn.health.AddHediff(modExtension.stage1Hediff);
            }
            if (this.pawn.genes.Xenotype == modExtension.stage2Xeno)
            {
                if (!this.pawn.health.hediffSet.HasHediff(modExtension.stage2Hediff))
                    pawn.health.AddHediff(modExtension.stage2Hediff);
            }
            if (this.pawn.genes.Xenotype == modExtension.stage3Xeno)
            {
                if (!this.pawn.health.hediffSet.HasHediff(modExtension.stage3Hediff))
                    pawn.health.AddHediff(modExtension.stage3Hediff);
            }
        }

        public override void PostRemove()
        {
            base.PostRemove();
            Ext_FungalChildren modExtension = def.GetModExtension<Ext_FungalChildren>();
            if (this.pawn.genes.Xenotype == modExtension.stage1Xeno)
            {
                pawn.health.RemoveHediff(this.pawn.health.hediffSet.GetFirstHediffOfDef(modExtension.stage1Hediff));
            }
            if (this.pawn.genes.Xenotype == modExtension.stage2Xeno)
            {
                pawn.health.RemoveHediff(this.pawn.health.hediffSet.GetFirstHediffOfDef(modExtension.stage2Hediff));
            }
            if (this.pawn.genes.Xenotype == modExtension.stage3Xeno)
            {
                pawn.health.RemoveHediff(this.pawn.health.hediffSet.GetFirstHediffOfDef(modExtension.stage3Hediff));
            }
        }
    }
}
