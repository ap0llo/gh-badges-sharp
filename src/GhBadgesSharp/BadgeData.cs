namespace GhBadgesSharp
{
    internal class BadgeData
    {
        public string LeftText { get; }

        public string RightText { get; }

        public string LeftLink { get; }

        public string RightLink { get; }

        public Color ColorA { get; }

        public Color ColorB { get; }


        public BadgeData(
            string leftText, string rightText,
            string leftLink, string rightLink,
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
