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
            return AccessTools.Method(typeof(Player.FirearmController.GClass1808), "OnMagAppeared");
        }

        [PatchPostfix]
        static void PatchPostfix(Player.FirearmController.GClass1808 __instance, Player.FirearmController ___firearmController_0)
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
            return AccessTools.Method(typeof(Player.FirearmController.GClass1785), "OnMagAppeared");
        }

        [PatchPostfix]
        static void PatchPostfix(Player.FirearmController.GClass1785 __instance, Player.FirearmController ___firearmController_0)
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
            return AccessTools.Method(typeof(Player.FirearmController.GClass1821), "OnModChanged");
        }

        [PatchPostfix]
        static void PatchPostfix
        (Player.FirearmController.GClass1821 __instance,
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
            return AccessTools.Method(typeof(Player.FirearmController.Class1145), "OnModChanged");
        }

        [PatchPostfix]
        static void PatchPostfix
        (Player.FirearmController.Class1145 __instance,
            Player.FirearmController ___firearmController_0)
        {
            var weaponPrefab = (WeaponPrefab)AccessTools.Field(___firearmController_0?.GetType(), "weaponPrefab_0")
                .GetValue(___firearmController_0);
            weaponPrefab.UpdateAnimatorHierarchy();
        }
    }
}
