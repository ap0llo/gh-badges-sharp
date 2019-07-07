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


        public IReadOnlyList<double> Widths { get; }


        public IReadOnlyList<string> Links { get; }


        public string Logo { get; }

        public int? LogoPosition { get; }

        public int? LogoWidth { get; }

        public int LogoPadding { get;}


        public string ColorA { get; }

        public string ColorB { get; }

        public IReadOnlyList<string> Colors { get; }

        public IReadOnlyList<string> EscapedColors { get; }

        // Computed properties to keep logic out of templates


        public string LeftLink => Links.Count > 0 ? NullIfEmptyString(Links[0]) : null;

        public string LeftLinkOrRightLink => Links.Count > 1 ? NullIfEmptyString(Links[1]) : LeftLink;



        public BadgeData(
            string templateName,
            string leftText, string rightText,
            double leftWidth, double rightWidth,
            string leftLink,  string rightLink,
            string logo, int? logoPosition, int? logoWidth, int logoPadding,
            string colorA, string colorB)
        {
            if (String.IsNullOrEmpty(templateName))
                throw new ArgumentException("Value must not be null or empty", nameof(templateName));

            TemplateName = templateName;

            LeftText = leftText;
            RightText = rightText;
            Text = new[] { leftText, rightText };
            TextLength = new[] { leftText?.Length ?? 0, rightText?.Length ?? 0 };
            EscapedText = new[] { EscapeXml(leftText), EscapeXml(rightText) };

            Widths = new[] { leftWidth, rightWidth };

            var links = new List<string>();
            links.AddIfNotNull(leftLink);
            links.AddIfNotNull(rightLink);

            Links = links;

            Logo = EscapeXml(logo);
            LogoPosition = logoPosition;
            LogoWidth = logoWidth;
            LogoPadding = logoPadding;

            ColorA = colorA;
            ColorB = colorB;
            Colors = new[] { colorA, colorB };
            EscapedColors = new[] { EscapeXml(colorA), EscapeXml(colorB) };
            
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
