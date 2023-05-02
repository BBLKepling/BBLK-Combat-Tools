using HarmonyLib;
using RimWorld;
using System.Linq;
using Verse;

namespace BBLKCombatTools
{
    [HarmonyPatch(typeof(PawnInventoryGenerator), nameof(PawnInventoryGenerator.GenerateInventoryFor))]
    class Harmony_PawnInventoryGenerator_GenerateInventoryFor_Postfix
    {
        [HarmonyPostfix]
        public static void GenerateInventoryFor(Pawn p)
        {
            if (p?.equipment?.Primary?.def is null || 
                p.inventory?.innerContainer is null || 
                !p.equipment.Primary.def.HasModExtension<GenerateWithAmmo>() || 
                p.equipment.Primary.def.GetModExtension<GenerateWithAmmo>().generateAmmo.NullOrEmpty()
                ) return;
            foreach (ThingDefCountRangeClass item in p.equipment.Primary.def.GetModExtension<GenerateWithAmmo>().generateAmmo)
            {
                if (p.equipment.Primary.Stuff != null)
                {
                    Thing thing = ThingMaker.MakeThing(item.thingDef, GenStuff.AllowedStuffsFor(item.thingDef).Any() ? p.equipment.Primary.Stuff : null);
                    thing.stackCount = item.countRange.RandomInRange;
                    p.inventory.innerContainer.TryAdd(thing);
                }
                else
                {
                    Thing thing = ThingMaker.MakeThing(item.thingDef, GenStuff.AllowedStuffsFor(item.thingDef).Any() ? GenStuff.AllowedStuffsFor(item.thingDef).RandomElement() : null);
                    thing.stackCount = item.countRange.RandomInRange;
                    p.inventory.innerContainer.TryAdd(thing);
                }
            }
        }
    }
}
