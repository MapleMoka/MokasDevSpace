using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_SeverityFromLight : HediffComp
    {
        private Gene_Resource_Light cachedHemogenGene;

        public HediffCompProperties_SeverityFromLight Props => (HediffCompProperties_SeverityFromLight)props;

        public override bool CompShouldRemove => base.Pawn.genes?.GetFirstGeneOfType<Gene_Resource_Light>() == null;

        private Gene_Resource_Light Hemogen => cachedHemogenGene ?? (cachedHemogenGene = base.Pawn.genes.GetFirstGeneOfType<Gene_Resource_Light>());

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (Hemogen != null)
            {
                severityAdjustment += ((Hemogen.Value > 0f) ? Props.severityPerHourLoaded : Props.severityPerHourEmpty) / 2500f;
            }
        }
    }
}
