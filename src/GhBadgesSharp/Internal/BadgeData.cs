using System;

namespace GhBadgesSharp.Internal
{
    internal class BadgeData
    {
        public string LeftText { get; }

        public string RightText { get; }

        public Uri LeftLink { get; }

        public Uri RightLink { get; }

        public Color ColorA { get; }

        public Color ColorB { get; }


        public BadgeData(
            string leftText, string rightText,
            Uri leftLink, Uri rightLink,
            Color colorA, Color colorB)
        {
            LeftText = leftText;
            RightText = rightText;

            LeftLink = leftLink;
            RightLink = rightLink;

            ColorA = colorA;
            ColorB = colorB;
        }
    }
}
