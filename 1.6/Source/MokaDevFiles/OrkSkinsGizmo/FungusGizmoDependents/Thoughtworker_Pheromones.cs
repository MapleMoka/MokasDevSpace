using RimWorld;
using Verse;

namespace MokaDevSpace
{
    internal class Thoughtworker_VoiceOfGod : ThoughtWorker
    {
        protected override ThoughtState CurrentStateInternal(Pawn p)
        {
            Gene_Resource_Fungus fungalGene = p.genes.GetFirstGeneOfType<Gene_Resource_Fungus>();
            //return base.CurrentStateInternal(p);
            if (fungalGene != null)
            {

                if (fungalGene.Value < 0.25)
                {
                    return ThoughtState.ActiveAtStage(0);
                }
                else if (fungalGene.Value < 0.5)
                {
                    return ThoughtState.ActiveAtStage(1);
                }
                else if (fungalGene.Value < 0.75)
                {
                    return ThoughtState.ActiveAtStage(2);
                }
                else
                {
                    return ThoughtState.ActiveAtStage(3);
                }
            }
            else
            {
                return ThoughtState.Inactive;
            }
        }
    }
}