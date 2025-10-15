using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;

namespace MFM
{
    internal class CompProperties_HarvestFromGene : CompProperties
    {
        public CompProperties_HarvestFromGene() => this.compClass = typeof(Comp_HarvestFromGene);
    }
}
