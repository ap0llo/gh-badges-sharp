﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace GhBadgesSharp.ViewModels
{
    internal abstract class ViewModelBase
    {
        private readonly BadgeData m_BadgeData;

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

        /// <summary>
        /// Gets the left/right width of the badge
        /// </summary>
        public virtual IReadOnlyList<double> Widths => m_BadgeData.Widths;


        protected ViewModelBase(BadgeData badgeData)
        {
            m_BadgeData = badgeData ?? throw new ArgumentNullException(nameof(badgeData));

            TextLength = new[] { m_BadgeData.LeftText?.Length ?? 0, badgeData.RightText?.Length ?? 0 };            
            Text = new[] { EscapeXml(NullIfEmptyString(m_BadgeData.LeftText)), EscapeXml(NullIfEmptyString(badgeData.RightText)) };

            Links = new[] { NullIfEmptyString(badgeData.LeftLink), NullIfEmptyString(badgeData.LeftLinkOrRightLink) };
            Colors = new[] { NullIfEmptyString(badgeData.ColorA), NullIfEmptyString(badgeData.ColorB) };

            Logo = NullIfEmptyString(m_BadgeData.Logo);
            LogoWidth = m_BadgeData.LogoWidth ?? 0;
            LogoPadding = m_BadgeData.LogoPadding;
        }



        protected static string EscapeXml(string value)
        {
            if (value == null)
                return null;

            return new XText(value).ToString();
        }

        protected static string NullIfEmptyString(string str) => String.IsNullOrEmpty(str) ? null : str;
    }
}
