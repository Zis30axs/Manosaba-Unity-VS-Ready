namespace Naninovel
{
    public interface IAddressableBuilder
    {
        void RemoveEntries ();
        bool TryAddEntry (string assetGuid, string resourcePath);
        void AssignScriptLabels ();
        void BuildContent ();
    }
}
