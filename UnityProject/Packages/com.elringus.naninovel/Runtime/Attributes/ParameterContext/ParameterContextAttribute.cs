using System;
using Naninovel.Metadata;

namespace Naninovel
{
    public abstract class ParameterContextAttribute : Attribute
    {
        public readonly ValueContextType Type;
        public readonly string SubType;
        public readonly string ParameterId;
        public readonly int Index;

        /// <param name="index">When applied to list or named parameter, specify index of the associated element (for named 0 is name and 1 is named value).</param>
        /// <param name="paramId">When attribute is applied to a class, specify parameter field name.</param>
        protected ParameterContextAttribute (ValueContextType type, string subType = null, int index = -1, string paramId = null)
        {
            Type = type;
            SubType = subType;
            ParameterId = paramId;
            Index = index;
        }
    }
}
