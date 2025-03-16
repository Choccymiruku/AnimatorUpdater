using BepInEx;

namespace UpdateHierarchy
{
    [BepInPlugin("com.choccy.updatehierarchy", "Choccy.AnimatorUpdate", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo("Animator Updater is loaded");
            new UpdateAnimatorPatch1().Enable();
            new UpdateAnimatorPatch2().Enable();
            new ModSetupPatch1().Enable();
            new ModSetupPatch2().Enable();
            //new InventoryReloadPatch().Enable();
        }
    }
}
