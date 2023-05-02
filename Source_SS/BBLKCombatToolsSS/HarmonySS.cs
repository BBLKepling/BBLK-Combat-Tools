using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using Verse;

namespace BBLKCombatToolsSS
{
    [HarmonyPatch(typeof(Verb_ShootOneUse), nameof(SelfConsume))]
    public static class Harmony_Verb_ShootOneUse_SelfConsume_Postfix
    {
        [HarmonyPostfix]
        public static void SelfConsume(Verb_ShootOneUse __instance)
        {
            if (!(__instance.caster is Pawn)) { return; }
            Pawn pawn = __instance.caster as Pawn;
            if (!pawn.equipment.GetDirectlyHeldThings().NullOrEmpty()) { return; }
            List<Thing> pawnInv = pawn.inventory?.innerContainer?.InnerListForReading;
            if (pawnInv.NullOrEmpty()) { return; }
            //pawnInv.Shuffle();
            //pawnInv.Reverse();
            foreach (Thing thing in pawnInv)
            {
                if (thing.def.IsRangedWeapon)
                {
                    pawn.inventory.innerContainer.TryTransferToContainer(thing, pawn.equipment.GetDirectlyHeldThings(), 1, false);
                    return;
                }
            }
            foreach (Thing thing in pawnInv)
            {
                if (thing.def.IsWeapon)
                {
                    pawn.inventory.innerContainer.TryTransferToContainer(thing, pawn.equipment.GetDirectlyHeldThings(), 1, false);
                    return;
                }
            }
        }
    }
}
