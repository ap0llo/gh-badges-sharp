using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Fluid;

namespace GhBadgesSharp
{
    internal static class Templates
    {
        private static readonly Dictionary<string, FluidTemplate> s_Templates = new Dictionary<string, FluidTemplate>();

        static Templates()
        {
            var assembly = Assembly.GetExecutingAssembly();

            var templateResourceNames = assembly
                .GetManifestResourceNames()
                .Where(name => name.StartsWith("GhBadgesSharp.Resources.Templates.") && name.EndsWith("-template.liquid"));

            foreach (var resourceName in templateResourceNames)
            {
                var templateName = resourceName
                    .Replace("GhBadgesSharp.Resources.Templates.", "")
                    .Replace("-template.liquid", "");

                using (var stream = assembly.GetManifestResourceStream(resourceName))
                using (var streamReader = new StreamReader(stream))
                {
                    var templateSource = streamReader.ReadToEnd();
                    var template = FluidTemplate.Parse(templateSource);
                    s_Templates.Add(templateName, template);
                }
            }
        }


        public static bool TemplateExists(string name) => s_Templates.ContainsKey(name);

        public static FluidTemplate GetTemplate(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            return s_Templates.TryGetValue(name, out var template)
                ? template
                : throw new TemplateNotFoundException(name);
        }

    }
}
