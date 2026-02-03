using Verse;

namespace MokaDevSpace
{

    public class Hediff_FungusCraving : HediffWithComps
    {
        public override string SeverityLabel
        {
            get
            {
                if (Severity == 0f)
                {
                    return null;
                }
                return Severity.ToStringPercent();
            }
        }
    }
}
