using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;

namespace MFM
{
    internal class CompProperties_ShearableGene : CompProperties
    {
        public CompProperties_ShearableGene() => this.compClass = typeof(Comp_HarvestFromGene);
    }
}
