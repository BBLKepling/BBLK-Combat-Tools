using HarmonyLib;
using Verse;

namespace BBLKCombatToolsSS
{
    [StaticConstructorOnStartup]
    public static class HarmonyInit
    {
        static HarmonyInit()
        {
            Harmony harmonyInstance = new Harmony("BBLKepling.CombatTools.SimpleSidearms");
            harmonyInstance.PatchAll();
        }
    }
}
