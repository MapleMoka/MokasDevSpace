using RimWorld;
using System;
using System.Collections.Generic;
using System.Text;
using Verse;

namespace MFM
{
    public class Building_BeeHive : Building
    {
        private int BeeHiveLevel;
        private int duration = 420000;
        public int tickBeforeTend = 120000;
        //public float neededFlower;
        private float progressPercentage;
        private int flowerCount;
        private int flowerNeeded;
        private List<IntVec3> cellsAround = new List<IntVec3>();
        private bool IsthereFlowerAround;

        public bool HoneyReady => this.BeeHiveLevel >= this.duration;

        public bool needTend => this.tickBeforeTend <= 0;

        private int EstimatedTicksLeft => this.duration - this.BeeHiveLevel;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look<int>(ref this.BeeHiveLevel, "ApiaryProgress");
            Scribe_Values.Look<int>(ref this.tickBeforeTend, "tickBeforeTend");
            Scribe_Values.Look<int>(ref this.flowerCount, "flowerCount");
            //Scribe_Values.Look<float>(ref this.neededFlower, "neededFlower");
        }

        public override void TickRare()
        {
            base.TickRare();
            this.progressPercentage = (float)this.BeeHiveLevel / (float)this.duration;
            this.IsthereFlowerAround = this.FlowerAround();
            this.flowerNeeded = this.FlowerNeeded();
            if ((double)this.AmbientTemperature < 10.0 || !this.IsthereFlowerAround)
                return;
            this.tickBeforeTend -= 250;
            if (!this.needTend)
                this.BeeHiveLevel += 250;
            else
                this.BeeHiveLevel += 125;
        }

        private void Reset() => this.BeeHiveLevel = 0;

        public void ResetTend(Pawn pawn)
        {
            if (new Random().Next(0, 21 - pawn.skills.skills.Find((Predicate<SkillRecord>)(r => r.def.defName == "Animals")).levelInt) == 1)
            {
                this.tickBeforeTend = 120000;
            }
            else
            {
                pawn.health.AddHediff(MFM_DefOf.Moka_BeeSting);
                pawn.needs.mood.thoughts.memories.TryGainMemoryFast(MFM_DefOf.Moka_BeeStingTht);
                MoteMaker.ThrowText(pawn.DrawPos, pawn.Map, "Tending failed", 5f);
            }
            pawn.skills.skills.Find((Predicate<SkillRecord>)(r => r.def.defName == "Animals")).Learn(100f, false);
        }

        public override string GetInspectString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(base.GetInspectString());
            if (stringBuilder.Length != 0)
                stringBuilder.AppendLine();
            if (this.HoneyReady)
                stringBuilder.AppendLine((string)"AReady".Translate());
            else if (!this.IsthereFlowerAround)
                stringBuilder.AppendLine((string)"ANeedFlower".Translate((NamedArgument)this.flowerNeeded));
            else if ((double)this.AmbientTemperature < 10.0)
            {
                stringBuilder.AppendLine((string)"AResting".Translate());
            }
            else
            {
                stringBuilder.AppendLine((string)"FermentationProgress".Translate((NamedArgument)this.progressPercentage.ToStringPercent(), (NamedArgument)this.EstimatedTicksLeft.ToStringTicksToPeriod()));
                if (this.needTend)
                    stringBuilder.AppendLine((string)"ANeedTend".Translate());
                else
                    stringBuilder.AppendLine((string)"ANeedTIn".Translate((NamedArgument)this.tickBeforeTend.ToStringTicksToPeriod()));
            }
            return stringBuilder.ToString().TrimEndNewlines();
        }

        public Thing TakeOutHoney()
        {
            if (!this.HoneyReady)
            {
                Log.Warning("Tried to get honey but it's not yet ready.");
                return (Thing)null;
            }
            Thing outHoney = ThingMaker.MakeThing(MFM_DefOf.Moka_Honey);
            outHoney.stackCount = 75;
            this.Reset();
            Thing outBeesWax = ThingMaker.MakeThing(MFM_DefOf.Moka_BeesWax);
            outBeesWax.stackCount = Rand.Chance(0.25f) ? 2 : 1;
            GenPlace.TryPlaceThing(outBeesWax, this.Position, this.Map, ThingPlaceMode.Near);
            return outHoney;
        }

        private int FlowerNeeded() => (this.cellsAround.Count - 8) / 2 - this.flowerCount;

        public bool FlowerAround()
        {
            this.flowerCount = 0;
            this.cellsAround = this.CellsAroundA(this.TrueCenter().ToIntVec3(), this.Map);
            foreach (IntVec3 c in this.cellsAround)
            {
                foreach (Thing thing in c.GetThingList(this.Map))
                {
                    if (thing is Plant plant)
                    {

                        if (plant.LifeStage != PlantLifeStage.Growing)
                        {
                            break;
                        }

                        //Grow plant equal to 1 day of growing.
                        var growthPerDay = 1f / plant.def.plant.growDays;
                        var growthPerTrigger = growthPerDay / 4;
                        plant.Growth += growthPerTrigger;

                        ++this.flowerCount;
                    }
                }
            }
            return this.flowerCount >= (this.cellsAround.Count - 8) / 2;
        }

        public List<IntVec3> CellsAroundA(IntVec3 pos, Map map)
        {
            List<IntVec3> intVec3List = new List<IntVec3>();
            if (!pos.InBounds(map))
                return intVec3List;
            foreach (IntVec3 cell in CellRect.CenteredOn(this.Position, 5).Cells)
            {
                if (cell.InHorDistOf(pos, 4.9f))
                    intVec3List.Add(cell);
            }
            return intVec3List;
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo g in base.GetGizmos())
                yield return g;
            if (Prefs.DevMode)
            {
                Command_Action gizmo1 = new Command_Action();
                gizmo1.defaultLabel = "Debug: Set progress to max";
                gizmo1.action = (Action)(() => this.BeeHiveLevel = this.duration);
                yield return (Gizmo)gizmo1;

                Command_Action gizmo2 = new Command_Action();
                gizmo2.defaultLabel = "Debug: Add progress";
                gizmo2.action = (Action)(() =>
                {
                    this.BeeHiveLevel += 12000;
                    this.tickBeforeTend -= 12000;
                });
                yield return (Gizmo)gizmo2;
            }
        }
    }
}
