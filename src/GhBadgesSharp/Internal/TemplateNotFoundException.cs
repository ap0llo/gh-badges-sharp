using System;

namespace Grynwald.GhBadgesSharp.Internal
{
    internal sealed class TemplateNotFoundException : Exception
    {
        public TemplateNotFoundException(string templateName) : base($"Template '{templateName}' does not exist")
        { }
    }
}
