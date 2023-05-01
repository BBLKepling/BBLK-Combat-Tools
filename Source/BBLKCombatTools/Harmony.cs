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
            if (p?.equipment != null)
            {
                foreach (ThingWithComps equip in p.equipment.AllEquipmentListForReading)
                {
                    if (!equip.def.HasModExtension<GenerateWithAmmo>()) { continue; }
                    if (equip.def.GetModExtension<GenerateWithAmmo>().generateAmmo.NullOrEmpty()) { continue; }
                    foreach (ThingDefCountRangeClass item in equip.def.GetModExtension<GenerateWithAmmo>().generateAmmo)
                    {
                        if (equip.Stuff != null)
                        {
                            Thing thing = ThingMaker.MakeThing(item.thingDef, GenStuff.AllowedStuffsFor(item.thingDef).Any() ? equip.Stuff : null);
                            thing.stackCount = item.countRange.RandomInRange;
                            p.inventory?.innerContainer.TryAdd(thing);
                        }
                        else
                        {
                            Thing thing = ThingMaker.MakeThing(item.thingDef, GenStuff.AllowedStuffsFor(item.thingDef).Any() ? GenStuff.AllowedStuffsFor(item.thingDef).RandomElement() : null);
                            thing.stackCount = item.countRange.RandomInRange;
                            p.inventory?.innerContainer.TryAdd(thing);
                        }
                    }
                }
            }
        }
    }
}
