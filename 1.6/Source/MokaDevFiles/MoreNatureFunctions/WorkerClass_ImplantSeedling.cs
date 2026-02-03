using System;
using System.Collections.Generic;
using System.Linq;
//using Genes40k;
using RimWorld;
using Verse;

namespace MokaDevSpace.MoreNatureFunctions
{

    // Genes40k, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
    // Genes40k.WorkerClass_ImplantGeneseed

    //public class WorkerClass_ImplantSeedling : Recipe_Surgery
    //{
    //    private GeneticSeedling geneseedVialForText;

    //    public override bool AvailableOnNow(Thing thing, BodyPartRecord part = null)
    //    {
    //        if (!base.AvailableOnNow(thing, part))
    //        {
    //            return false;
    //        }
    //        if (!(thing is Pawn { Spawned: not false } pawn))
    //        {
    //            return false;
    //        }
    //        //if (pawn.IsSuperHuman())
    //        //{
    //        //    return false;
    //        //}
    //        //if (pawn.UndergoingPhaseDevelopment())
    //        //{
    //        //    return false;
    //        //}
    //        //if (pawn.story != null && pawn.story.traits.HasTrait(Genes40kDefOf.BEWH_Serf))
    //        //{
    //        //    return false;
    //        //}
    //        DefModExtension_GeneseedVialRecipe modExtension = recipe.GetModExtension<DefModExtension_GeneseedVialRecipe>();
    //        foreach (Thing item in pawn.Map.listerThings.ThingsOfDef(modExtension.geneseedVial))
    //        {
    //            if (item is GeneticSeedling geneseedVial && modExtension.geneFromMaterial == geneseedVial.extraGeneFromMaterial)
    //            {
    //                geneseedVialForText = geneseedVial;
    //                return true;
    //            }
    //        }
    //        return false;
    //    }

    //    //public override TaggedString GetConfirmation(Pawn pawn)
    //    //{
    //    //    return Genes40kUtils.GetGeneseedImplantationSuccessChanceDesc(pawn, geneseedVialForText);
    //    //}

    //    public override void ApplyOnPawn(Pawn pawn, BodyPartRecord part, Pawn billDoer, List<Thing> ingredients, Bill bill)
    //    {
    //        if (!CheckSurgeryFail(billDoer, pawn, ingredients, part, bill))
    //        {
    //            GeneticSeedling geneseedVial = (GeneticSeedling)ingredients.First((Thing x) => x is GeneticSeedling);
    //            ImplantGeneseed(pawn, geneseedVial);
    //            if (IsViolationOnPawn(pawn, part, Faction.OfPlayer))
    //            {
    //                ReportViolation(pawn, billDoer, pawn.HomeFaction, -70);
    //            }
    //        }
    //    }

    //    private static void ImplantGeneseed(Pawn pawn, GeneticSeedling geneseedVial)
    //    {
    //        DefModExtension_GeneseedVial modExtension = geneseedVial.def.GetModExtension<DefModExtension_GeneseedVial>();
    //        //int geneseedImplantationSuccessChance = Genes40kUtils.GetGeneseedImplantationSuccessChance(pawn, geneseedVial);
    //        //if (new Random().Next(0, 100) < geneseedImplantationSuccessChance)
    //        //{
    //        //    pawn.Kill(null, null);
    //        //    return;
    //        //}
    //        pawn.genes.SetXenotypeDirect(modExtension.xenotype);
    //        if (modExtension.overrideXenotypeGenesGiven)
    //        {
    //            foreach (GeneDef item in modExtension.overridenAddedGenes.Where((GeneDef gene) => !pawn.genes.HasActiveGene(gene)))
    //            {
    //                pawn.genes.AddGene(item, xenogene: true);
    //            }
    //        }
    //        else
    //        {
    //            foreach (GeneDef item2 in modExtension.xenotype.genes.Where((GeneDef gene) => !pawn.genes.HasActiveGene(gene)))
    //            {
    //                pawn.genes.AddGene(item2, xenogene: true);
    //            }
    //        }
    //        //if (geneseedVial.extraGeneFromMaterial != null)
    //        //{
    //        //    pawn.genes.AddGene(geneseedVial.extraGeneFromMaterial, xenogene: true);
    //        //}
    //        if (modExtension.appliesHediff != null)
    //        {
    //            pawn.health.AddHediff(modExtension.appliesHediff);
    //        }
    //    }
    //}

}
