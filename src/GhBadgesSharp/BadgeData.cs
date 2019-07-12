using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace GhBadgesSharp
{
    public class BadgeData
    {
        public string Logo { get; }

        public string LeftText { get; }

        public string RightText { get; }

        public int? LogoPosition { get; }

        public int? LogoWidth { get; }

        public int LogoPadding { get; }


        public string ColorA { get; }

        public string ColorB { get; }

        public string LeftLink { get; }

        public string RightLink { get; }



        public BadgeData(
            string leftText, string rightText,
            string leftLink, string rightLink,
            string logo, int? logoPosition, int? logoWidth, int logoPadding,
            string colorA, string colorB)
        {          
            

            Logo = EscapeXml(logo);
            LeftText = leftText;
            RightText = rightText;
            LogoPosition = logoPosition;
            LogoWidth = logoWidth;
            LogoPadding = logoPadding;

            LeftLink = leftLink;
            RightLink = rightLink;

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
