using BepInEx;
using EFT;
using System;
using SPT.Reflection.Patching;
using System.Reflection;
using UnityEngine;
using SPT.Reflection.Utils;
using HarmonyLib;
using System.Linq;
using EFT.InventoryLogic;

namespace UpdateHierarchy
{
    public class UpdateAnimatorPatch1 : ModulePatch
    {
        
        //i have 0 clue on what this guy does. But seems like not used
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
    
    //this patch is the one that has effect on reloading, but also desync the hands on certain weapons i do not know why.
    //Note for future: if you want a if check that uses array, do not use a loop. Thanks CJ for helping me out
    public class UpdateAnimatorPatch2 : ModulePatch
    {
        public static readonly string[] ExcludeList = 
        [ "5ba26383d4351e00334c93d9", 
            "5ac4cd105acfc40016339859", 
            "5de652c31b7e3716273428be"
        ];

        public static readonly String MP7 = "5ba26383d4351e00334c93d9";
        public static readonly String AK74M = "5ac4cd105acfc40016339859";
        public static readonly String Gornostay = "5de652c31b7e3716273428be";
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
                var weapon = (Weapon)AccessTools.Field(weaponPrefab.GetType(), "weapon_0").GetValue(weaponPrefab);
                if (ExcludeList.Contains(weapon.Template._id.ToString()))
                {
                    return;
                }
                weaponPrefab.UpdateAnimatorHierarchy();
                //Logger.LogInfo("This method is supposed to run when weapon outside of the list is used");
                
                /*if(weapon.Template._id != MP7 || weapon.Template._id != AK74M || weapon.Template._id != Gornostay)
                {
                    weaponPrefab.UpdateAnimatorHierarchy();
                    Logger.LogInfo("This method is supposed to run when weapon outside of the list is used");
                }*/
            }
        }
    }

    //This one does not have effect
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
    
    //This one has effect
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
            var weapon = (Weapon)AccessTools.Field(weaponPrefab.GetType(), "weapon_0").GetValue(weaponPrefab);
            if (UpdateAnimatorPatch2.ExcludeList.Contains(weapon.Template._id.ToString()))
            {
                return;
            }
            weaponPrefab.UpdateAnimatorHierarchy();
            //Logger.LogInfo("This method is supposed to run when weapon outside of the list is used");
            /*if(weapon.Template._id != UpdateAnimatorPatch2.MP7 || 
               weapon.Template._id != UpdateAnimatorPatch2.AK74M || 
               weapon.Template._id != UpdateAnimatorPatch2.Gornostay)
            {
                weaponPrefab.UpdateAnimatorHierarchy();
                Logger.LogInfo("This method is supposed to run when weapon outside of the list is used");
            }*/
        }
    }
}
