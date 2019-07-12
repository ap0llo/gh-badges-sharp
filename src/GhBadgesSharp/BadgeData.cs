using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace GhBadgesSharp
{
    public class BadgeData
    {
        public IReadOnlyList<string> Links { get; }


        public string Logo { get; }

        public string LeftText { get; }

        public string RightText { get; }

        public int? LogoPosition { get; }

        public int? LogoWidth { get; }

        public int LogoPadding { get; }


        public string ColorA { get; }

        public string ColorB { get; }

        public string LeftLink => Links.Count > 0 ? NullIfEmptyString(Links[0]) : null;

        public string LeftLinkOrRightLink => Links.Count > 1 ? NullIfEmptyString(Links[1]) : LeftLink;



        public BadgeData(
            string leftText, string rightText,
            string leftLink, string rightLink,
            string logo, int? logoPosition, int? logoWidth, int logoPadding,
            string colorA, string colorB)
        {          
            var links = new List<string>();
            links.AddIfNotNull(leftLink);
            links.AddIfNotNull(rightLink);


            Links = links;

            Logo = EscapeXml(logo);
            LeftText = leftText;
            RightText = rightText;
            LogoPosition = logoPosition;
            LogoWidth = logoWidth;
            LogoPadding = logoPadding;

            ColorA = colorA;
            ColorB = colorB;
        }


        private string NullIfEmptyString(string str) => String.IsNullOrEmpty(str) ? null : str;

        private static string EscapeXml(string value)
        {
            if (value == null)
            {
                return null;
            }

            return new XText(value).ToString();
        }


    }
}
