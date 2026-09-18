using System.Collections.Generic;
using Naninovel.Parsing;

namespace Naninovel
{
    public class MixedValueParser
    {
        private readonly List<RawValuePart> parts = new();
        private readonly ITextIdentifier identifier;

        public MixedValueParser (ITextIdentifier identifier)
        {
            this.identifier = identifier;
        }

        /// <summary>
        /// Parses specified mixed script value into raw parameter value.
        /// </summary>
        /// <param name="mixed">Mixed scenario script value to parse.</param>
        /// <param name="hashPlainText">Whether to parse <see cref="PlainText"/> as <see cref="IdentifiedText"/> mapped by value hash.</param>
        public RawValue Parse (MixedValue mixed, bool hashPlainText)
        {
            parts.Clear();
            foreach (var component in mixed)
                if (component is PlainText plain)
                    if (hashPlainText) parts.Add(HashPlainText(plain));
                    else parts.Add(RawValuePart.FromPlainText(plain));
                else if (component is IdentifiedText idText) parts.Add(RawValuePart.FromIdentifiedText(idText.Id.Body));
                else if (component is Parsing.Expression expression) parts.Add(RawValuePart.FromExpression(expression.Body));
            return new(parts.ToArray());
        }

        private RawValuePart HashPlainText (PlainText plain)
        {
            var hash = ScriptTextIdentifier.VolatilePrefix + CryptoUtils.PersistentHexCode(plain.Text);
            identifier.Identify(hash, plain.Text);
            return RawValuePart.FromIdentifiedText(hash);
        }
    }
}
