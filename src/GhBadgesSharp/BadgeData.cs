using System;
using System.Collections.Generic;
using System.Text;

namespace GhBadgesSharp
{
    public class BadgeData
    {
        public string LeftText { get; internal set; }
        public string RightText { get; internal set; }
        public string EscapedLeftText { get; internal set; }
        public string EscapedRightText { get; internal set; }
        public double[] Widths { get; internal set; }
        public IEnumerable<string> Links { get; internal set; }
        public string Logo { get; internal set; }
        public int? LogoPosition { get; internal set; }
        public int? LogoWidth { get; internal set; }
        public int LogoPadding { get; internal set; }
        public string ColorA { get; internal set; }
        public string ColorB { get; internal set; }
        public string TemplateName { get; internal set; }
    }
}
