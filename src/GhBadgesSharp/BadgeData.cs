using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhBadgesSharp
{
    public class BadgeData
    {
        public string TemplateName { get; internal set; }

        public string LeftText { get; internal set; }

        public string RightText { get; internal set; }

        public string EscapedLeftText { get; internal set; }

        public string EscapedRightText { get; internal set; }

        public double[] Widths { get; internal set; }

        public IReadOnlyList<string> Links { get; internal set; }

        public string Logo { get; internal set; }

        public int? LogoPosition { get; internal set; }

        public int? LogoWidth { get; internal set; }

        public int LogoPadding { get; internal set; }

        public string ColorA { get; internal set; }

        public string ColorB { get; internal set; }


        // Computed properties to keep logic out of templates


        public string LeftLink => Links.Count > 0 ? NullIfEmptyString(Links[0]) : null;

        public string LeftLinkOrRightLink => Links.Count > 1 ? NullIfEmptyString(Links[1]) : LeftLink;



        private string NullIfEmptyString(string str) => String.IsNullOrEmpty(str) ? null : str;

    }
}
