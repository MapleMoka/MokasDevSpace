using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace MokaDevSpace
{
    internal class IngestionOutcomeDoer_OffsetFungus : IngestionOutcomeDoer
    {
        public float offset;

        protected override void DoIngestionOutcomeSpecial(Pawn pawn, Thing ingested, int ingestedCount)
        {
            OrkUtils.OffsetFungus(pawn, offset * (float)ingestedCount);
        }

        public override IEnumerable<StatDrawEntry> SpecialDisplayStats(ThingDef parentDef)
        {
            if (ModsConfig.BiotechActive)
            {
                string text = ((offset >= 0f) ? "+" : string.Empty);
                yield return new StatDrawEntry(StatCategoryDefOf.BasicsNonPawnImportant, "Fungus".Translate().CapitalizeFirst(), text + Mathf.RoundToInt(offset * 100f), "FungusDesc".Translate(), 1000);
            }
        }
    }
}