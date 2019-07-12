namespace GhBadgesSharp
{
    public class BadgeData
    {
        public string LeftText { get; }

        public string RightText { get; }

        public string LeftLink { get; }

        public string RightLink { get; }

        public string Logo { get; }

        public int? LogoPosition { get; }

        public int? LogoWidth { get; }

        public int LogoPadding { get; }

        public string ColorA { get; }

        public string ColorB { get; }


        public BadgeData(
            string leftText, string rightText,
            string leftLink, string rightLink,
            string logo, int? logoPosition, int? logoWidth, int logoPadding,
            string colorA, string colorB)
        {
            LeftText = leftText;
            RightText = rightText;

            LeftLink = leftLink;
            RightLink = rightLink;

            Logo = logo;
            LogoPosition = logoPosition;
            LogoWidth = logoWidth;
            LogoPadding = logoPadding;

            ColorA = colorA;
            ColorB = colorB;
        }
    }
}
