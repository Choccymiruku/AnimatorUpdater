using BepInEx;
using EFT;
using System;
using SPT.Reflection.Patching;
using System.Reflection;
using UnityEngine;
using SPT.Reflection.Utils;
using HarmonyLib;
using System.Linq;

namespace UpdateHierarchy
{
    public class UpdateAnimatorPatch1 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(Player.FirearmController.GClass1751), "OnMagAppeared");
        }

        [PatchPostfix]
        static void PatchPostfix(Player.FirearmController.GClass1751 __instance, Player.FirearmController ___firearmController_0)
        {
            if (___firearmController_0 != null)
            {
               var weaponPrefab = (WeaponPrefab) AccessTools.Field(___firearmController_0.GetType(), "weaponPrefab_0").GetValue(___firearmController_0);
                weaponPrefab.UpdateAnimatorHierarchy();
            }
        }
    }
    
    public class UpdateAnimatorPatch2 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(Player.FirearmController.GClass1773), "OnMagAppeared");
        }

        [PatchPostfix]
        static void PatchPostfix(Player.FirearmController.GClass1773 __instance, Player.FirearmController ___firearmController_0)
        {
            if (___firearmController_0 != null)
            {
                var weaponPrefab = (WeaponPrefab) AccessTools.Field(___firearmController_0.GetType(), "weaponPrefab_0").GetValue(___firearmController_0);
                weaponPrefab.UpdateAnimatorHierarchy();
            }
        }
    }

    public class ModSetupPatch1 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(Player.FirearmController.GClass1789), "OnModChanged");
        }

        [PatchPostfix]
        static void PatchPostfix
        (Player.FirearmController.GClass1789 __instance,
            Player.FirearmController ___firearmController_0)
        {
            var weaponPrefab = (WeaponPrefab)AccessTools.Field(___firearmController_0?.GetType(), "weaponPrefab_0")
                .GetValue(___firearmController_0);
            weaponPrefab.UpdateAnimatorHierarchy();
        }
    }
    
    public class ModSetupPatch2 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(Player.FirearmController.Class1131), "OnModChanged");
        }

        [PatchPostfix]
        static void PatchPostfix
        (Player.FirearmController.Class1131 __instance,
            Player.FirearmController ___firearmController_0)
        {
            var weaponPrefab = (WeaponPrefab)AccessTools.Field(___firearmController_0?.GetType(), "weaponPrefab_0")
                .GetValue(___firearmController_0);
            weaponPrefab.UpdateAnimatorHierarchy();
        }
    }
}
