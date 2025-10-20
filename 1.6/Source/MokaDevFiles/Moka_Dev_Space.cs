using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace MokaDevSpace
{
    [StaticConstructorOnStartup]
    public static class Moka_Dev_Space
    {
        static Moka_Dev_Space()
        {
            Log.Message("Moka's Dev Environment Successfully Loaded!");
        }
    }
}
