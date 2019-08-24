using System.Collections.Generic;
using Fluid;

namespace GhBadgesSharp.ViewModels
{
    /// <summary>
    /// View model for the "for-the-badge" template
    /// </summary>
    /// <remarks>
    /// Template ported from https://github.com/badges/shields/blob/66c7f13e38c76bfdbdb96107bc92e607b3dd5acc/gh-badges/templates/for-the-badge-template.svg
    /// </remarks>
    internal class ForTheBadgeViewModel : ViewModelBase
    {
        private readonly double[] m_Widths;

        /// <summary>
        /// Gets the width of the image (the value for 'width' attribute of the root 'svg' element)
        /// </summary>
        public double ImageWidth { get; }


        public IReadOnlyList<double> Widths => m_Widths;


        public IReadOnlyList<string> Fill { get; }

        /// <summary>
        /// Gets the position of the text elements in the svg (to be used as value for the 'x' and 'y' attributes in the svg)
        /// </summary>
        public IReadOnlyList<Point> TextPosition { get; }

        /// <summary>
        /// Gets the width of the text in the svg (to be used as value for the 'textLength' attribute in the svg)
        /// </summary>
        public IReadOnlyList<double> TextWidth { get; }


        public ForTheBadgeViewModel(BadgeData badgeData) : base(badgeData, badgeData.LeftText?.ToUpper(), badgeData.RightText?.ToUpper())
        {

            m_Widths = new double[2];

            // ImageWidth (it.widths[0] -= it.text[0].length ? -(10+(it.text[0].length*1.5)) : (it.logo ? (it.colorA ? -7 : 7) : 11))+(it.widths[1]+=(10+(it.text[1].length*2)))
            // widths: [leftWidth + 10 + logoWidth + logoPadding, rightWidth + 10]

            m_Widths[0] = (m_LeftTextWidth + 10) - (Text[0] != null ? -(10 + ((Text[0]?.Length ?? 0) * 1.5)) : 11);
            m_Widths[1] = (m_RightTextWidth + 10) + (10 + ((Text[1]?.Length ?? 0) * 2));

            ImageWidth = m_Widths[0] + m_Widths[1];

            // =it.escapeXml(it.text[0].length || it.logo && it.colorA ? (it.colorA||"#555") : (it.colorB||"#4c1"))
            var fill1 = Text[0] != null ? (m_BadgeData.ColorA?.SvgName ?? "#555") : (m_BadgeData.ColorB?.SvgName ?? "#4c1");

            // =it.escapeXml(it.colorB||"#4c1")
            var fill2 = m_BadgeData.ColorB?.SvgName ?? "#4c1";

            Fill = new[] { EscapeXml(fill1), EscapeXml(fill2) };

            // value in original template: =((it.widths[0]+it.logoWidth+it.logoPadding)/2)*10
            var x1 = (Widths[0] / 2) * 10;

            // value in original template: =(it.widths[0]+it.widths[1]/2)*10
            var x2 = (Widths[0] + Widths[1] / 2) * 10;

            TextPosition = new[] { new Point(x1, 0), new Point(x2, 0) };

            // value in original template: =(it.widths[0]-(24+it.logoWidth+it.logoPadding))*10
            var textWidth1 = (Widths[0] - 24) * 10;

            // value in original template: =(it.widths[1]-24)*10
            var textWidth2 = (Widths[1] - 24) * 10;

            TextWidth = new[] { textWidth1, textWidth2 };
        }

        internal override FluidTemplate GetTemplate() => Templates.GetTemplate("for-the-badge");
    }
}
