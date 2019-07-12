using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Fluid;

namespace GhBadgesSharp.ViewModels
{
    internal abstract class ViewModelBase
    {
        protected readonly BadgeData m_BadgeData;
        protected readonly double m_LeftTextWidth;
        protected readonly double m_RightTextWidth;

        /// <summary>
        /// Gets the left/right text (xml-escaped)
        /// </summary>
        public IReadOnlyList<string> Text { get; }

        /// <summary>
        /// Gets the left/right link
        /// </summary>
        public IReadOnlyList<string> Links { get; }

        /// <summary>
        /// Gets the badge's logo
        /// </summary>
        public string Logo => EscapeXml(NullIfEmptyString(m_BadgeData.Logo));

        /// <summary>
        /// Gets the width of the badge's logo
        /// </summary>
        public int LogoWidth => m_BadgeData.LogoWidth ?? 0;

        /// <summary>
        /// Gets the padding of the badge's logo
        /// </summary>
        public int LogoPadding => m_BadgeData.LogoPadding;

        public int? LogoPosition => m_BadgeData.LogoPosition;



        protected ViewModelBase(BadgeData badgeData)
        {
            m_BadgeData = badgeData ?? throw new ArgumentNullException(nameof(badgeData));

            m_LeftTextWidth = GetTextWidth(m_BadgeData.LeftText);
            m_RightTextWidth = GetTextWidth(m_BadgeData.RightText);

            Text = new[] { EscapeXml(m_BadgeData.LeftText), EscapeXml(m_BadgeData.RightText) };

            Links = new[] { NullIfEmptyString(badgeData.LeftLink), NullIfEmptyString(badgeData.RightLink) ?? NullIfEmptyString(badgeData.LeftLink) };
        }


        internal abstract FluidTemplate GetTemplate();


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

            var width = TextWidth.Get(text) / 10;

            // Increase chances of pixel grid alignment.
            if (width % 2 == 0)
            {
                width++;
            }

            return width;
        }
    }
}
