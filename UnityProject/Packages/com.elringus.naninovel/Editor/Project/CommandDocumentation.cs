namespace Naninovel
{
    public readonly struct CommandDocumentation
    {
        public string Summary { get; }
        public string Remarks { get; }
        public string[] Examples { get; }

        public CommandDocumentation (string summary, string remarks, string[] examples)
        {
            Summary = summary;
            Remarks = remarks;
            Examples = examples;
        }
    }
}
