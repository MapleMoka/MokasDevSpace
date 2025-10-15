using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Verse;

namespace MFM
{
    internal class CompProperties_MilkableGene : CompProperties
    {
        public CompProperties_MilkableGene() => this.compClass = typeof(CompMilkableGene);
    }
}
