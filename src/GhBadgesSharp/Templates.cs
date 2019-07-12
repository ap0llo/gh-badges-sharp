using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fluid;

namespace GhBadgesSharp
{
    internal static class Templates
    {
        private const string s_TemplateResourcePrefix = "GhBadgesSharp.Resources.Templates.";
        private const string s_TemplateResourceSuffix = "-template.liquid";
        private static readonly Dictionary<string, Lazy<FluidTemplate>> s_Templates = new Dictionary<string, Lazy<FluidTemplate>>();

        static Templates()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var templateResourceNames = assembly
                .GetManifestResourceNames()
                .Where(name => name.StartsWith(s_TemplateResourcePrefix) && name.EndsWith(s_TemplateResourceSuffix));

            foreach (var resourceName in templateResourceNames)
            {
                var templateName = resourceName
                    .Replace(s_TemplateResourcePrefix, "")
                    .Replace(s_TemplateResourceSuffix, "");


                s_Templates.Add(templateName, new Lazy<FluidTemplate>(() =>
                {
                    var templateSource = ResourceHelper.LoadEmbeddedResource(resourceName);
                    return FluidTemplate.Parse(templateSource);                        
                }));

            }
        }

        public static FluidTemplate GetTemplate(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return s_Templates.TryGetValue(name, out var template)
                ? template.Value
                : throw new TemplateNotFoundException(name);
        }
    }
}
