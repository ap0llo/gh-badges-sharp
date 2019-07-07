using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace GhBadgesSharp
{
    public class BadgeData
    {
        public string TemplateName { get;}

        public string LeftText { get;  }

        public string RightText { get; }

        public IReadOnlyList<string> Text { get; }

        public IReadOnlyList<int> TextLength { get; }

        public IReadOnlyList<string> EscapedText { get; }


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



        public BadgeData(string templateName, string leftText, string rightText)
        {
            if (String.IsNullOrEmpty(templateName))
                throw new ArgumentException("Value must not be null or empty", nameof(templateName));

            TemplateName = templateName;

            LeftText = leftText;
            RightText = rightText;

            Text = new[] { leftText, rightText };
            TextLength = new[] { leftText?.Length ?? 0, rightText?.Length ?? 0 };
            EscapedText = new[] { EscapeXml(leftText), EscapeXml(rightText) };
        }


        private string NullIfEmptyString(string str) => String.IsNullOrEmpty(str) ? null : str;

        private static string EscapeXml(string value)
        {
            if (value == null)
                return null;

            return new XText(value).ToString();
        }


    }
}
