using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_SeverityFromLumens : HediffComp
    {
        private Gene_Resource_Lumens cachedLumenGene;

        public HediffCompProperties_SeverityFromLumens Props => (HediffCompProperties_SeverityFromLumens)props;

        public override bool CompShouldRemove => base.Pawn.genes?.GetFirstGeneOfType<Gene_Resource_Lumens>() == null;

        private Gene_Resource_Lumens Lumen => cachedLumenGene ?? (cachedLumenGene = base.Pawn.genes.GetFirstGeneOfType<Gene_Resource_Lumens>());

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (Lumen != null)
            {
                severityAdjustment += ((Lumen.Value > 0f) ? Props.severityPerHourLoaded : Props.severityPerHourEmpty) / 2500f;
            }
        }
    }
}
