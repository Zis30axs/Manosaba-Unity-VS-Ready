namespace Naninovel.Commands
{
    [Doc(
        @"
Sets an [unlockable item](/guide/unlockable-items) with the specified ID to `unlocked` state.",
        @"
The unlocked state of the items is stored in [global scope](/guide/state-management#global-state).<br/>
In case item with the specified ID is not registered in the global state map,
the corresponding record will automatically be added.",
        @"
; Unlocks an unlockable CG record with ID 'FightScene1'.
@unlock CG/FightScene1"
    )]
    public class Unlock : Command
    {
        [Doc("ID of the unlockable item. Use `*` to unlock all the registered unlockable items.")]
        [ParameterAlias(NamelessParameterAlias), RequiredParameter, ResourceContext(UnlockablesConfiguration.DefaultPathPrefix)]
        public StringParameter Id;

        public override async UniTask Execute (AsyncToken token = default)
        {
            var unlockableManager = Engine.GetServiceOrErr<IUnlockableManager>();

            if (Id.Value.EqualsFastIgnoreCase("*")) unlockableManager.UnlockAllItems();
            else unlockableManager.UnlockItem(Id);

            await Engine.GetServiceOrErr<IStateManager>().SaveGlobal();
        }
    }
}
