using System.Collections.Generic;
using Fluid;

namespace GhBadgesSharp.ViewModels
{
    /// <summary>
    /// View model for the "social" template
    /// </summary>
    /// <remarks>
    /// Template ported from https://github.com/badges/shields/blob/66c7f13e38c76bfdbdb96107bc92e607b3dd5acc/gh-badges/templates/for-the-badge-template.svg
    /// </remarks>
    internal class SocialViewModel : ViewModelBase
    {
        private readonly double[] m_Widths = new double[2];

        /// <summary>
        /// Gets the width of the image (the value for 'width' attribute of the root 'svg' element)
        /// </summary>
        public double ImageWidth { get; }

        public IReadOnlyList<double> Widths => m_Widths;

        /// <summary>
        /// Gets the position of the text elements in the svg (to be used as value for the 'x' and 'y' attributes in the svg)
        /// </summary>
        public IReadOnlyList<Point> TextPosition { get; }

        /// <summary>
        /// Gets the width of the text in the svg (to be used as value for the 'textLength' attribute in the svg)
        /// </summary>
        public IReadOnlyList<double> TextWidth { get; }


        public SocialViewModel(BadgeData badgeData) : base(badgeData, Capitalize(badgeData.LeftText))
        {
            // original template: 
            //  ImageWidth:  it.widths[0] + 1 + (it.text[1] && it.text[1].length > 0 ? it.widths[1]+2 : 0)
            //  widths: [leftWidth + 10 + logoWidth + logoPadding, rightWidth + 10]

            m_Widths[0] = (m_LeftTextWidth + 10 + LogoWidth + LogoPadding);
            m_Widths[1] = m_RightTextWidth + 10;

            ImageWidth = m_Widths[0] + 1 + (Text[1] != null && Text[1].Length > 0 ? m_Widths[1] + 2 : 0);

            // As in the original template, decrement widths[1] by four after calculating image width
            // {{it.widths[1]-=4;}}
            m_Widths[1] -= 4;

            {
                // value in original template: {{=((it.widths[0]+it.logoWidth+it.logoPadding)/2)*10}}
                var x1 = ((m_Widths[0] + LogoWidth + LogoPadding) / 2) * 10;

                // value in original template: {{=(it.widths[0]+it.widths[1]/2+6)*10}}
                var x2 = (m_Widths[0] + m_Widths[1] / 2 + 6) * 10;

                TextPosition = new[] { new Point(x1, 0), new Point(x2, 0) };
            }
            {
                // value in original template: {{=(it.widths[0]-(10+it.logoWidth+it.logoPadding))*10}}
                var textWidth1 = (Widths[0] - (10 + LogoWidth + LogoPadding)) * 10;

                // value in original template: {{=(it.widths[1]-8)*10}}
                var textWidth2 = (Widths[1] - 8) * 10;

                TextWidth = new[] { textWidth1, textWidth2 };
            }
        }


        internal override FluidTemplate GetTemplate() => Templates.GetTemplate("social");


        private static string Capitalize(string value)
        {
            if (value == null)
            {
                return null;
            }

            if (value.Length == 0)
            {
                return value;
            }

            if (value.Length == 1)
            {
                return value.ToUpper();
            }

            return value.Substring(0, 1).ToUpper() + value.Substring(1);
        }
    }
}
