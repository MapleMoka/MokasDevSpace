using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_SeverityFromToxons : HediffComp
    {
        private Gene_Resource_Toxon cachedToxonGene;

        public HediffCompProperties_SeverityFromToxons Props => (HediffCompProperties_SeverityFromToxons)props;

        public override bool CompShouldRemove => base.Pawn.genes?.GetFirstGeneOfType<Gene_Resource_Toxon>() == null;

        private Gene_Resource_Toxon Toxon => cachedToxonGene ?? (cachedToxonGene = base.Pawn.genes.GetFirstGeneOfType<Gene_Resource_Toxon>());

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (Toxon != null)
            {
                severityAdjustment += ((Toxon.Value > 0f) ? Props.severityPerHourLoaded : Props.severityPerHourEmpty) / 2500f;
            }
        }
    }
}
