using System;

namespace Naninovel
{
    public readonly struct ParameterInfo : IEquatable<ParameterInfo>
    {
        public ICommandParameter Instance { get; }
        public string Id { get; }
        public string Alias { get; }
        public string DefaultValue { get; }
        public bool Required { get; }
        public bool Nameless => Alias == Command.NamelessParameterAlias;

        public ParameterInfo (ICommandParameter instance, string id,
            string alias = null, string defaultValue = null, bool required = false)
        {
            Instance = instance;
            Id = id;
            Alias = alias;
            DefaultValue = defaultValue;
            Required = required;
        }

        public bool Equals (ParameterInfo other)
        {
            return Id == other.Id;
        }

        public override bool Equals (object obj)
        {
            return obj is ParameterInfo other && Equals(other);
        }

        public override int GetHashCode ()
        {
            return Id != null ? Id.GetHashCode() : 0;
        }
    }
}
