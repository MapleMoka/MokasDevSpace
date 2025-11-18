using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_SeverityFromPollution : HediffComp
    {
        private Gene_Resource_Pollution cachedHemogenGene;

        public HediffCompProperties_SeverityFromPollution Props => (HediffCompProperties_SeverityFromPollution)props;

        public override bool CompShouldRemove => base.Pawn.genes?.GetFirstGeneOfType<Gene_Resource_Pollution>() == null;

        private Gene_Resource_Pollution Hemogen => cachedHemogenGene ?? (cachedHemogenGene = base.Pawn.genes.GetFirstGeneOfType<Gene_Resource_Pollution>());

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (Hemogen != null)
            {
                severityAdjustment += ((Hemogen.Value > 0f) ? Props.severityPerHourLoaded : Props.severityPerHourEmpty) / 2500f;
            }
        }
    }
}
