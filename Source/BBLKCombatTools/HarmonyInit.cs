using HarmonyLib;
using Verse;

namespace BBLKCombatTools
{
    [StaticConstructorOnStartup]
    public static class HarmonyInit
    {
        static HarmonyInit()
        {
            Harmony harmonyInstance = new Harmony("BBLKepling.CombatTools");
            harmonyInstance.PatchAll();
        }
    }
}
