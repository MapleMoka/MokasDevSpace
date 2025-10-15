using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MFM
{
    internal class Placeworker_BeeHive : PlaceWorker
    {
        public override AcceptanceReport AllowsPlacing(
          BuildableDef def,
          IntVec3 center,
          Rot4 rot,
          Map map,
          Thing thingToIgnore = null,
          Thing thing = null)
        {
            CellRect cellRect = GenAdj.OccupiedRect(center, rot, def.Size);
            cellRect = cellRect.ExpandedBy(1);
            foreach (IntVec3 c in cellRect)
            {
                List<Thing> thingList = map.thingGrid.ThingsListAt(c);
                for (int index = 0; index < thingList.Count; ++index)
                {
                    Thing thing1 = thingList[index];
                    if (thing1 != thingToIgnore && (thing1.def.category == ThingCategory.Building && thing1.def.defName == "Moka_BeeHive" || (thing1.def.IsBlueprint || thing1.def.IsFrame) && thing1.def.entityDefToBuild is ThingDef && thing1.def.entityDefToBuild.defName == "Moka_BeeHive"))
                        return (AcceptanceReport)"APlaceWorker".Translate();
                }
            }
            return center.Roofed(map) ? (AcceptanceReport)"APlaceWorkerNoRoof".Translate() : (AcceptanceReport)true;
        }
    }
}
