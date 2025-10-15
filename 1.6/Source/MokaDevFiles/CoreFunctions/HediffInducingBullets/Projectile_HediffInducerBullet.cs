using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MCM
{
    internal class Projectile_HediffInducerBullet : Bullet
    {

        public Ext_HediffInducerBullet Props => def.GetModExtension<Ext_HediffInducerBullet>();

        protected override void Impact(Thing hitThing, bool blockedByShield = false)
        {
            base.Impact(hitThing, blockedByShield);
            if (Props != null && hitThing != null && hitThing is Pawn hitPawn)
            {
                float rand = Rand.Value;
                if (rand <= Props.addHediffChance)
                {
                    //Messages.Message("Moka_HeddifInducerBullet_Success".Translate(this.launcher.Label, hitPawn.Label, Props.hediffToAdd), MessageTypeDefOf.NeutralEvent);
                    Hediff hediffOnPawn = hitPawn.health?.hediffSet?.GetFirstHediffOfDef(Props.hediffToAdd);
                    if (hediffOnPawn != null)
                    {
                        hediffOnPawn.Severity += Props.addHediffSeverity;
                    }
                    else
                    {
                        Hediff hediff = HediffMaker.MakeHediff(Props.hediffToAdd, hitPawn);
                        hediff.Severity = Props.addHediffSeverity;
                        hitPawn.health.AddHediff(hediff);
                    }
                }
                //else
                //{
                //MoteMaker.ThrowText(hitThing.PositionHeld.ToVector3(), hitThing.MapHeld, "TST_PlagueBullet_FailureMote".Translate(Props.addHediffChance), 12f);
                //}
            }
        }
    }
}