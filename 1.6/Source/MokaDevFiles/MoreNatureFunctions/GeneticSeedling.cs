using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace MokaDevSpace.MoreNatureFunctions
{

    // Genes40k, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
    // Genes40k.GeneseedVial

    //[StaticConstructorOnStartup]
    //public class GeneticSeedling : ThingWithComps
    //{
    //    protected GeneSet geneSet;

    //    public string xenotypeName;

    //    public XenotypeDef xenotype;

    //    public XenotypeIconDef iconDef;

    //    public GeneDef extraGeneFromMaterial;

    //    private bool invisible;

    //    private static readonly CachedTexture GeneticInfoTex = new CachedTexture("UI/Gizmos/ViewGenes");

    //    private const int MaxGeneLabels = 5;

    //    private List<string> tmpGeneLabelsDesc = new List<string>();

    //    private List<string> tmpGeneLabels = new List<string>();

    //    public GeneSet GeneSet => geneSet;

    //    public override string DescriptionDetailed
    //    {
    //        get
    //        {
    //            tmpGeneLabelsDesc.Clear();
    //            string text = base.DescriptionDetailed;
    //            if (geneSet == null || !geneSet.GenesListForReading.Any())
    //            {
    //                return text;
    //            }
    //            if (!text.NullOrEmpty())
    //            {
    //                text += "\n\n";
    //            }
    //            foreach (GeneDef item in geneSet.GenesListForReading)
    //            {
    //                tmpGeneLabelsDesc.Add(item.label);
    //            }
    //            return text + ("Genes".Translate().CapitalizeFirst() + ":\n" + tmpGeneLabelsDesc.ToLineList("  - ", capitalizeItems: true));
    //        }
    //    }

    //    public override string LabelNoCount
    //    {
    //        get
    //        {
    //            if (xenotypeName.NullOrEmpty())
    //            {
    //                return base.LabelNoCount;
    //            }
    //            return "BEWH.MankindsFinest.GeneseedVial.NamedGeneseedVial".Translate(xenotypeName.Named("NAME"));
    //        }
    //    }

    //    public override Graphic Graphic
    //    {
    //        get
    //        {
    //            //IL_0011: Unknown result type (might be due to invalid IL or missing references)
    //            //IL_0037: Unknown result type (might be due to invalid IL or missing references)
    //            //IL_0025: Unknown result type (might be due to invalid IL or missing references)
    //            //IL_003c: Unknown result type (might be due to invalid IL or missing references)
    //            Graphic copy = base.DefaultGraphic.GetCopy(def.graphicData.drawSize, null);
    //            copy.drawSize = ((!invisible) ? def.graphicData.drawSize : Vector2.zero);
    //            return copy;
    //        }
    //    }

    //    public override void PostMake()
    //    {
    //        base.PostMake();
    //        geneSet = new GeneSet();
    //        Initialize();
    //    }

    //    public void ChangeVisibility(bool newValue)
    //    {
    //        invisible = newValue;
    //    }

    //    public void Initialize()
    //    {
    //        DefModExtension_GeneseedVial modExtension = def.GetModExtension<DefModExtension_GeneseedVial>();
    //        if (!modExtension.xenotype.genes.NullOrEmpty())
    //        {
    //            foreach (GeneDef gene in modExtension.xenotype.genes)
    //            {
    //                geneSet.AddGene(gene);
    //            }
    //        }
    //        xenotype = modExtension.xenotype;
    //        xenotypeName = modExtension.xenotype.label;
    //        iconDef = modExtension.xenotypeIcon;
    //    }

    //    public override IEnumerable<Gizmo> GetGizmos()
    //    {
    //        foreach (Gizmo gizmo in base.GetGizmos())
    //        {
    //            yield return gizmo;
    //        }
    //        if (geneSet != null)
    //        {
    //            yield return new Command_Action
    //            {
    //                defaultLabel = "InspectGenes".Translate() + "...",
    //                defaultDesc = "InspectGenesEmbryoDesc".Translate(),
    //                icon = (Texture)(object)GeneticInfoTex.Texture,
    //                action = delegate
    //                {
    //                    Genes40kUtils.InspectGeneseedVialGenes(this);
    //                }
    //            };
    //        }
    //    }

    //    public override string GetInspectString()
    //    {
    //        string text = base.GetInspectString();
    //        tmpGeneLabels.Clear();
    //        if (geneSet == null || !geneSet.GenesListForReading.Any())
    //        {
    //            return text;
    //        }
    //        if (!text.NullOrEmpty())
    //        {
    //            text += "\n";
    //        }
    //        List<GeneDef> genesListForReading = geneSet.GenesListForReading;
    //        int num = Mathf.Min(5, genesListForReading.Count);
    //        for (int i = 0; i < num; i++)
    //        {
    //            string text2 = genesListForReading[i].label;
    //            if (geneSet.IsOverridden(genesListForReading[i]))
    //            {
    //                text2 += " (" + "Overridden".Translate() + ")";
    //            }
    //            tmpGeneLabels.Add(text2);
    //        }
    //        if (genesListForReading.Count > num)
    //        {
    //            tmpGeneLabels.Add("Etc".Translate() + "...");
    //        }
    //        return text + ("Genes".Translate().CapitalizeFirst() + ":\n" + tmpGeneLabels.ToLineList("  - ", capitalizeItems: true));
    //    }

    //    public override IEnumerable<StatDrawEntry> SpecialDisplayStats()
    //    {
    //        foreach (StatDrawEntry item in base.SpecialDisplayStats())
    //        {
    //            yield return item;
    //        }
    //        if (geneSet == null)
    //        {
    //            yield break;
    //        }
    //        Dialog_InfoCard.Hyperlink? inspectGenesHyperlink = null;
    //        if (ThingSelectionUtility.SelectableByMapClick(this))
    //        {
    //            inspectGenesHyperlink = new Dialog_InfoCard.Hyperlink(this, -1, thingIsGeneOwner: true);
    //        }
    //        foreach (StatDrawEntry item2 in geneSet.SpecialDisplayStats(inspectGenesHyperlink))
    //        {
    //            yield return item2;
    //        }
    //    }

    //    public override void ExposeData()
    //    {
    //        base.ExposeData();
    //        Scribe_Values.Look(ref xenotypeName, "xenotypeName");
    //        Scribe_Defs.Look(ref xenotype, "xenotype");
    //        Scribe_Defs.Look(ref iconDef, "iconDef");
    //        Scribe_Defs.Look(ref extraGeneFromMaterial, "extraGeneFromMaterial");
    //        Scribe_Deep.Look(ref geneSet, "geneSet");
    //        Scribe_Values.Look(ref invisible, "invisible", defaultValue: false);
    //        if (iconDef == null && Scribe.mode == LoadSaveMode.PostLoadInit)
    //        {
    //            iconDef = XenotypeIconDefOf.Basic;
    //        }
    //    }
    //}

}
