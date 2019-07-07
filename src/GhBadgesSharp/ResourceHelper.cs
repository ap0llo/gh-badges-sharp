using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace GhBadgesSharp
{
    internal static class ResourceHelper
    {
        public static string LoadEmbeddedResource(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentException("Value must not be null or empty", nameof(name));

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();                
            }
        }

    }
}
