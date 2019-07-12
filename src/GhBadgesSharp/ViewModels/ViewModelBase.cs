using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace GhBadgesSharp.ViewModels
{
    internal abstract class ViewModelBase
    {
        private readonly string m_LeftText;
        private readonly string m_RightText;
        private readonly BadgeData m_BadgeData;
        protected readonly double m_LeftTextWidth;
        protected readonly double m_RightTextWidth;

        /// <summary>
        /// Gets the left/right text (xml-escaped)
        /// </summary>
        public IReadOnlyList<string> Text { get; }

        /// <summary>
        /// Gets the length (number of characters) of the left/right text elements
        /// </summary>
        public IReadOnlyList<int> TextLength { get; }

        /// <summary>
        /// Gets the left/right link
        /// </summary>
        public IReadOnlyList<string> Links { get; }

        /// <summary>
        /// Gets the badge's logo
        /// </summary>
        public string Logo { get; }

        /// <summary>
        /// Gets the width of the badge's logo
        /// </summary>
        public int LogoWidth { get; }

        /// <summary>
        /// Gets the padding of the badge's logo
        /// </summary>
        public int LogoPadding { get; }

        /// <summary>
        /// Gets the left/right colors for the badge
        /// </summary>
        public IReadOnlyList<string> Colors { get; }


        protected ViewModelBase(BadgeData badgeData)
        {
            m_LeftText = NullIfEmptyString(badgeData.LeftText);
            m_RightText = NullIfEmptyString(badgeData.RightText);
            m_BadgeData = badgeData ?? throw new ArgumentNullException(nameof(badgeData));

            m_LeftTextWidth = GetTextWidth(m_LeftText);
            m_RightTextWidth = GetTextWidth(m_RightText);

            TextLength = new[] { m_LeftText?.Length ?? 0, m_RightText?.Length ?? 0 };
            Text = new[] { EscapeXml(m_LeftText), EscapeXml(m_RightText) };

            Links = new[] { NullIfEmptyString(badgeData.LeftLink), NullIfEmptyString(badgeData.LeftLinkOrRightLink) };
            Colors = new[] { NullIfEmptyString(badgeData.ColorA), NullIfEmptyString(badgeData.ColorB) };

            Logo = NullIfEmptyString(m_BadgeData.Logo);
            LogoWidth = m_BadgeData.LogoWidth ?? 0;
            LogoPadding = m_BadgeData.LogoPadding;
        }



        protected static string EscapeXml(string value)
        {
            if (value == null)
            {
                return null;
            }

            return new XText(value).ToString();
        }

        protected static string NullIfEmptyString(string str) => str == null ? null : (String.IsNullOrEmpty(str) ? null : str);


        protected static double GetTextWidth(string text)
        {
            if (text == null)
                return 0d;

            var width = TextWidthHelper.GetWidth(text) / 10;

            // Increase chances of pixel grid alignment.
            if (width % 2 == 0)
            {
                width++;
            }

            return width;
        }
    }
}
