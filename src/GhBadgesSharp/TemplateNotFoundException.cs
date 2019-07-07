using System;

namespace GhBadgesSharp
{
    public sealed class TemplateNotFoundException : Exception
    {
        public TemplateNotFoundException(string templateName) : base($"Template '{templateName}' does not exist")
        { }
    }
}
