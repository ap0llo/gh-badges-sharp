using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fluid;

namespace Grynwald.GhBadgesSharp.Internal
{
    internal static class Templates
    {
        private const string s_TemplateResourcePrefix = "Grynwald.GhBadgesSharp.Resources.Templates.";
        private const string s_TemplateResourceSuffix = "-template.liquid";
        private static readonly Dictionary<string, Lazy<IFluidTemplate>> s_Templates = new Dictionary<string, Lazy<IFluidTemplate>>();

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


                s_Templates.Add(templateName, new Lazy<IFluidTemplate>(() =>
                {
                    var templateSource = EmbeddedResource.Load(resourceName);
                    var parser = new FluidParser();
                    return parser.Parse(templateSource);
                }));

            }
        }

        public static IFluidTemplate GetTemplate(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return s_Templates.TryGetValue(name, out var template)
                ? template.Value
                : throw new TemplateNotFoundException(name);
        }
    }
}
