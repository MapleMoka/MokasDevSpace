using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{

    public class Gene_RandomGeneFromList : Gene
    {
        public List<GeneDef> genesList = new List<GeneDef>();

        public override void PostAdd()
        {
            base.PostAdd();

            genesList = def.GetModExtension<Ext_RandomGeneFromList>().genesToChooseFrom;
            if (genesList.Count > 0)
            {
                pawn.genes.AddGene(genesList.RandomElement(), false);

            }
            pawn.genes.RemoveGene(this);

        }

    }
}
