namespace Naninovel
{
    public class MockAddressableBuilder : IAddressableBuilder
    {
        public void RemoveEntries () { }
        public bool TryAddEntry (string assetGuid, string resourcePath) => false;
        public void AssignScriptLabels () { }
        public void BuildContent () { }
    }
}
