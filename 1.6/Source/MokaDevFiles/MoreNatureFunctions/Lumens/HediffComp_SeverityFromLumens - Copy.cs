using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    internal class HediffComp_SawSeed : HediffComp
    {
        private Gene_Resource_Lumens cachedLumenGene;

        public HediffCompProperties_SawSeed Props => (HediffCompProperties_SawSeed)props;

        //public override bool CompShouldRemove => base.Pawn.genes?.GetFirstGeneOfType<Gene_Resource_Lumens>() == null;

        private Gene_Resource_Lumens Lumen => cachedLumenGene ?? (cachedLumenGene = base.Pawn.genes.GetFirstGeneOfType<Gene_Resource_Lumens>());

        public override void CompPostTick(ref float severityAdjustment)
        {
            if (Lumen == null)
            {
                //severityAdjustment += ((this.parent.pawn.PawnInLight()) ? Props.severityPerHourInSun : Props.severityPerHourNotInSun) / 2500f;
                if (this.parent.pawn.PawnInLight())
                {
                    this.parent.Severity += Props.severityPerHourInSun / 2500;
                }
                else
                {
                    this.parent.Severity += Props.severityPerHourNotInSun / 2500;
                }
            }
            if (Lumen != null)
            {
                if (Lumen.Value > 0f)
                {
                    this.parent.Severity += Props.severityPerHourWithLumen / 2500;
                    Lumen.Value -= Props.lumenDrainPerSeverityOffset / 2500;
                }
                else
                {
                    //severityAdjustment += ((this.parent.pawn.PawnInLight()) ? Props.severityPerHourInSun : Props.severityPerHourNotInSun) / 2500f;
                    if (this.parent.pawn.PawnInLight())
                    {
                        this.parent.Severity += Props.severityPerHourInSun / 2500;
                    }
                    else
                    {
                        this.parent.Severity += Props.severityPerHourNotInSun / 2500;
                    }
                }
                    //severityAdjustment += ((Lumen.Value > 0f) ? Props.severityPerHourLoaded : Props.severityPerHourEmpty) / 2500f;
            }
        }
    }
}
