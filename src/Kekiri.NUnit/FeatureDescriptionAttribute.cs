using System;
using NUnit.Framework;

namespace Kekiri.NUnit
{
    [AttributeUsage(AttributeTargets.Field)]
    public class FeatureDescriptionAttribute : CategoryAttribute, IFeatureDescriptionAttribute
    {
        public string[] Details { get; private set; }

        public string Summary { get; private set; }

        public FeatureDescriptionAttribute(string summary, params string [] details)
            : base(summary)
        {
            Summary = summary;
            Details = details ?? new string[0];
        }
    }
}