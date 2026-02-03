using System.Collections.Generic;
using Verse;

namespace MokaDevSpace
{
    public class Ext_AddHediffToBody : DefModExtension
    {
        public HediffDef hediffToAdd;
        public List<Sub_Ext_HediffToBodyparts> hediffsToApply;
    }
    public class Sub_Ext_HediffToBodyparts
    {
        public List<BodyPartDef> bodyParts;
        public HediffDef hediff;
    }
}
