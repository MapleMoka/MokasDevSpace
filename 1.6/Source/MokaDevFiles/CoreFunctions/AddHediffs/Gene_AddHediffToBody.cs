using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    public class Gene_AddHediffToBody : Gene
    {

        public override void PostAdd()
        {
            base.PostAdd();
            Ext_AddHediffToBody modExtension = def.GetModExtension<Ext_AddHediffToBody>();

            if (modExtension.hediffToAdd != null)
            {
                pawn.health.AddHediff(modExtension.hediffToAdd);
            }
            if (modExtension.hediffsToApply != null)
            {
                foreach (Sub_Ext_HediffToBodyparts hediffsToBodyPart in modExtension.hediffsToApply)
                {
                    int index = 0;
                    foreach (BodyPartDef bodypart in hediffsToBodyPart.bodyParts)
                    {
                        if (!pawn.RaceProps.body.GetPartsWithDef(bodypart).EnumerableNullOrEmpty<BodyPartRecord>() && index <= pawn.RaceProps.body.GetPartsWithDef(bodypart).Count)
                        {
                            pawn.health.AddHediff(hediffsToBodyPart.hediff, pawn.RaceProps.body.GetPartsWithDef(bodypart).ToArray()[index]);
                            ++index;
                        }
                    }
                }
            }
        }
        public override void PostRemove()
        {
            base.PostRemove();
            Ext_AddHediffToBody modExtension = def.GetModExtension<Ext_AddHediffToBody>();

            if (modExtension.hediffToAdd != null)
            {
                pawn.health.RemoveHediff(this.pawn.health.hediffSet.GetFirstHediffOfDef(modExtension.hediffToAdd));
            }
            if (modExtension.hediffsToApply != null)
            {
                foreach (Sub_Ext_HediffToBodyparts hediffsToBodyPart in modExtension.hediffsToApply)
                {
                    foreach (BodyPartDef bodypart in hediffsToBodyPart.bodyParts)
                    {
                        HediffSet hediffSet = pawn.health.hediffSet;
                        if (hediffSet != null && hediffSet.HasHediff(hediffsToBodyPart.hediff))
                        {
                            Hediff firstHediffOfDef = pawn.health.hediffSet.GetFirstHediffOfDef(hediffsToBodyPart.hediff);
                            if (firstHediffOfDef != null)
                                pawn.health.RemoveHediff(firstHediffOfDef);
                        }
                    }
                }
            }
        }
    }
}
