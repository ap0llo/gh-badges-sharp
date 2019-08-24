using System;

namespace GhBadgesSharp
{
    internal sealed class TemplateNotFoundException : Exception
    {
        public TemplateNotFoundException(string templateName) : base($"Template '{templateName}' does not exist")
        { }
    }
}
