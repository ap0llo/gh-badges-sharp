using System;
using System.IO;
using System.Reflection;

namespace Grynwald.GhBadgesSharp.Internal
{
    internal static class EmbeddedResource
    {
        public static string Load(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Value must not be null or empty", nameof(name));
            }

            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            using (var streamReader = new StreamReader(stream))
            {
                return streamReader.ReadToEnd();
            }
        }

    }
}
