using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_ToxonStinger : HediffComp
    {
        private Gene_Resource_Toxon cachedToxonGene;

        public HediffCompProperties_ToxonStinger Props => (HediffCompProperties_ToxonStinger)props;

        public override bool CompShouldRemove => base.Pawn.genes?.GetFirstGeneOfType<Gene_Resource_Toxon>() == null;

        private Gene_Resource_Toxon Toxon => cachedToxonGene ?? (cachedToxonGene = base.Pawn.genes.GetFirstGeneOfType<Gene_Resource_Toxon>());

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (this.parent.Severity >= 0.9f)
            {
                if (this.parent.pawn.health.hediffSet.HasHediff(MCM_DefOf.Moka_StingerNumb))
                    this.parent.pawn.health.RemoveHediff(this.parent.pawn.health.hediffSet.GetFirstHediffOfDef(MCM_DefOf.Moka_StingerNumb));
            }
            if (this.parent.Severity <= 0.001)
            {
                if (!this.parent.pawn.health.hediffSet.HasHediff(MCM_DefOf.Moka_StingerNumb))
                    this.parent.pawn.health.AddHediff(MCM_DefOf.Moka_StingerNumb);
            }
            //if (Toxon != null)
            //{
            //    severityAdjustment += ((Toxon.Value > 0f) ? Props.severityPerHourLoaded : Props.severityPerHourEmpty) / 2500f;
            //}
        }
    }
}
