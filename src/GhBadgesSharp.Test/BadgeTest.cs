using System;
using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Xunit;
using Xunit.Abstractions;

namespace GhBadgesSharp.Test
{
    [UseReporter(typeof(DiffReporter))]
    public partial class BadgeTest
    {
        private class ApprovalNamer : UnitTestFrameworkNamer
        {
            private readonly int m_Id;

            public override string Name => $"{base.Name}_{m_Id:000}";

            public ApprovalNamer(int id)
            {
                m_Id = id;
            }

            public override string GetSubdirectory() => "/testdata";
        }



        private class BadgeTestData : IXunitSerializable
        {
            public BadgeStyle Style { get; private set; }

            public string LeftText { get; private set; }

            public string RightText { get; private set; }

            public string Color { get; private set; }

            public string LabelColor { get; private set; }

            public string LeftLink { get; private set; }

            public string RightLink { get; private set; }

            public BadgeTestData(BadgeStyle style, string leftText, string rightText, string color, string labelColor, string leftLink, string rightLink)
            {
                Style = style;
                LeftText = leftText;
                RightText = rightText;
                Color = color;
                LabelColor = labelColor;
                LeftLink = leftLink;
                RightLink = rightLink;
            }

            // parameterless constructor required by xunit serialization
            public BadgeTestData()
            { }


            public void Deserialize(IXunitSerializationInfo info)
            {
                Style = Enum.Parse<BadgeStyle>(info.GetValue<string>(nameof(Style)));
                LeftText = info.GetValue<string>(nameof(LeftText));
                RightText = info.GetValue<string>(nameof(RightText));
                Color = info.GetValue<string>(nameof(Color));
                LabelColor = info.GetValue<string>(nameof(LabelColor));
                LeftLink = info.GetValue<string>(nameof(LeftLink));
                RightLink = info.GetValue<string>(nameof(RightLink));
            }

            public void Serialize(IXunitSerializationInfo info)
            {
                info.AddValue(nameof(Style), Style.ToString());
                info.AddValue(nameof(LeftText), LeftText);
                info.AddValue(nameof(RightText), RightText);
                info.AddValue(nameof(Color), Color);
                info.AddValue(nameof(LabelColor), LabelColor);
                info.AddValue(nameof(LeftLink), LeftLink);
                info.AddValue(nameof(RightLink), RightLink);
            }
        }


        [Theory]
        //          id  template                leftText   rightText  color       labelColor    logo    logoPosition    logoWidth    link1                  link2
        [InlineData(1, BadgeStyle.Flat, "Hello", "World", "yellow", null, null, null, null, null, null)]
        [InlineData(2, BadgeStyle.Flat, "Hello", "World", "yellow", null, null, null, null, "http://example.com", null)]
        [InlineData(3, BadgeStyle.Plastic, "Hello", "World", "yellow", null, null, null, null, null, null)]
        [InlineData(4, BadgeStyle.Plastic, "Hello", "World", "yellow", null, null, null, null, "http://example.com", null)]
        [InlineData(5, BadgeStyle.Flat, "Hello", "World", "yellow", null, null, null, null, "http://example.com", "https://www.github.com")]
        [InlineData(6, BadgeStyle.Plastic, "Hello", "World", "yellow", null, null, null, null, "http://example.com", "https://www.github.com")]
        [InlineData(7, BadgeStyle.Plastic, "Hello", "World", "yellow", null, null, null, null, "", null)]
        [InlineData(8, BadgeStyle.Flat, "Hello", "World", "yellow", null, null, null, null, "", null)]
        [InlineData(9, BadgeStyle.Flat, "Hello", "World", "red", "blue", null, null, null, null, null)]
        [InlineData(10, BadgeStyle.Plastic, "Hello", "World", "red", "blue", null, null, null, null, null)]
        [InlineData(11, BadgeStyle.FlatSquare, "Hello", "World", "yellow", null, null, null, null, null, null)]
        [InlineData(12, BadgeStyle.FlatSquare, "Hello", "World", "yellow", null, null, null, null, "http://example.com", null)]
        [InlineData(13, BadgeStyle.FlatSquare, "Hello", "World", "yellow", null, null, null, null, "http://example.com", "https://www.github.com")]
        [InlineData(14, BadgeStyle.FlatSquare, "Hello", "World", "yellow", null, null, null, null, "", null)]
        [InlineData(15, BadgeStyle.FlatSquare, "Hello", "World", "red", "blue", null, null, null, null, null)]
        [InlineData(16, BadgeStyle.ForTheBadge, "Hello", "World", "yellow", null, null, null, null, null, null)]
        [InlineData(17, BadgeStyle.ForTheBadge, "Hello", "World", "yellow", null, null, null, null, "http://example.com", null)]
        [InlineData(18, BadgeStyle.ForTheBadge, "Hello", "World", "yellow", null, null, null, null, "http://example.com", "https://www.github.com")]
        [InlineData(19, BadgeStyle.ForTheBadge, "Hello", "World", "yellow", null, null, null, null, "", null)]
        [InlineData(20, BadgeStyle.ForTheBadge, "Hello", "World", "red", "blue", null, null, null, null, null)]
        public void MakeBadge_returns_expected_svg(int id, BadgeStyle style, string leftText, string rightText, string color, string labelColor, string logo, int? logoPosition, int? logoWidth, string link1, string link2)
        {
            var badge = Badge.MakeBadge(style, leftText, rightText, color, labelColor, logo, logoPosition, logoWidth, link1, link2);
            var writer = new ApprovalTextWriter(badge.ToString(), "svg");

            Approvals.Verify(writer, new ApprovalNamer(id), Approvals.GetReporter());
        }

    }
}
