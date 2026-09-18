namespace Naninovel
{
    /// <inheritdoc cref="TransientSaveSlotManager"/>
    public class TransientGameStateSerializer : TransientSaveSlotManager<GameStateMap>
    {
        public TransientGameStateSerializer (StateConfiguration config, string savesFolderPath) { }
    }

    /// <inheritdoc cref="TransientSaveSlotManager"/>
    public class TransientGlobalStateSerializer : TransientSaveSlotManager<GlobalStateMap>
    {
        public TransientGlobalStateSerializer (StateConfiguration config, string savesFolderPath) { }
    }

    /// <inheritdoc cref="TransientSaveSlotManager"/>
    public class TransientSettingsStateSerializer : TransientSaveSlotManager<SettingsStateMap>
    {
        public TransientSettingsStateSerializer (StateConfiguration config, string savesFolderPath) { }
    }
}
