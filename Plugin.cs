using BepInEx;

namespace UpdateHierarchy
{
    [BepInPlugin("com.choccy.updatehierarchy", "Choccy.AnimatorUpdate", "1.0.2")]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo("Animator Updater is loaded");
            //new UpdateAnimatorPatch1().Enable(); This one does not have any effect
            new UpdateAnimatorPatch2().Enable(); //this one has it but desync the hand
            //new ModSetupPatch1().Enable(); //this one has no effect
            new ModSetupPatch2().Enable(); //this one have the effect
            //new InventoryReloadPatch().Enable();
        }
    }
}
