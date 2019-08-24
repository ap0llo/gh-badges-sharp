using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Fluid;

namespace GhBadgesSharp.Internal.ViewModels
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


        protected ViewModelBase(BadgeData badgeData, string leftTextOverride = null, string rightTextOverride = null)
        {
            m_BadgeData = badgeData ?? throw new ArgumentNullException(nameof(badgeData));

            // if left or right text is empty, replace it with null.
            // This makes checking the value in the liquid templates easier
            // as they do not need to check for null *or* empty
            // Both cases are treated the same anyways
            var leftText = NullIfEmptyString(leftTextOverride ?? badgeData.LeftText);
            var rightText = NullIfEmptyString(rightTextOverride ?? badgeData.RightText);

            m_LeftTextWidth = GetTextWidth(leftText);
            m_RightTextWidth = GetTextWidth(rightText);

            Text = new[] { EscapeXml(leftText), EscapeXml(rightText) };

            Links = new[] { NullIfEmptyString(badgeData.LeftLink?.ToString()), NullIfEmptyString(badgeData.RightLink?.ToString()) ?? NullIfEmptyString(badgeData.LeftLink?.ToString()) };
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
