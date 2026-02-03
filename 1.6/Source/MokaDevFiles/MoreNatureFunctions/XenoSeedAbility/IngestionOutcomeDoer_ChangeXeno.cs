using RimWorld;
using System.Collections.Generic;
using Verse;

namespace MokaDevSpace
{
    internal class IngestionOutcomeDoer_ChangeXeno :IngestionOutcomeDoer
    {
        //public List<RandomXenotype> xenotypes;
        public XenotypeDef xenotype;
        public List<XenotypeDef> xenotypesCheck;
        //public ThingDef filth;
        //public IntRange filthCount;
        //public bool setXenotype;
        //public bool sendMessage;

        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested, int ingestedCount)
        {
            if (this.xenotype == null)
                return;
            if (this.xenotypesCheck == null)
                return;
            if (this.xenotypesCheck.Contains(pawn.genes.Xenotype))
            {
                Messages.Message((string)"MokaDevSpace.ConversionImpossible".Translate(pawn.Named("PAWN"), (NamedArgument)pawn.genes.Xenotype.label, (NamedArgument)xenotype.label), (LookTargets)(Thing)pawn, MessageTypeDefOf.NeutralEvent, true);
                return;
            }
                
            //pawn.AlterXenotype(this.xenotypes, this.filth, this.filthCount, this.setXenotype, this.sendMessage);
            pawn.genes.SetXenotypeDirect(this.xenotype);
            for (int index = 0; index < xenotype.genes.Count; ++index)
                pawn.genes.AddGene(xenotype.genes[index], !xenotype.inheritable);
            Messages.Message((string)"MokaDevSpace.ConversionComplete".Translate(pawn.Named("PAWN"), (NamedArgument)xenotype.label, (NamedArgument)ingested.Label), (LookTargets)(Thing)pawn, MessageTypeDefOf.NeutralEvent, true);
        }
        /*
        public IngestionOutcomeDoer_ChangeXeno()
        {
            this.filth = ThingDefOf.Filth_AmnioticFluid;
            this.filthCount = new IntRange(4, 7);
            //this.setXenotype = true;
            this.sendMessage = true;
            //base.\u002Ector();
            base.GetType().GetConstructor(new Type[] { typeof(Pawn) });
        } */

    }
}
