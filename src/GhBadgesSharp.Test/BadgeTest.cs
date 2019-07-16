using System.Linq;
using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Xunit;

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


        [Theory]
        //          id  template                leftText   rightText  color       labelColor    logo    logoPosition    logoWidth    link1                  link2
        [InlineData(1,  BadgeStyle.Flat,        "Hello",   "World",   "yellow",   null,         null,   null,           null,        null,                  null)]
        [InlineData(2,  BadgeStyle.Flat,        "Hello",   "World",   "yellow",   null,         null,   null,           null,        "http://example.com",  null)]
        [InlineData(3,  BadgeStyle.Plastic,     "Hello",   "World",   "yellow",   null,         null,   null,           null,        null,                  null)]
        [InlineData(4,  BadgeStyle.Plastic,     "Hello",   "World",   "yellow",   null,         null,   null,           null,        "http://example.com",  null)]
        [InlineData(5,  BadgeStyle.Flat,        "Hello",   "World",   "yellow",   null,         null,   null,           null,        "http://example.com",  "https://www.github.com")]
        [InlineData(6,  BadgeStyle.Plastic,     "Hello",   "World",   "yellow",   null,         null,   null,           null,        "http://example.com",  "https://www.github.com")]
        [InlineData(7,  BadgeStyle.Plastic,     "Hello",   "World",   "yellow",   null,         null,   null,           null,        "",                    null)]
        [InlineData(8,  BadgeStyle.Flat,        "Hello",   "World",   "yellow",   null,         null,   null,           null,        "",                    null)]
        [InlineData(9,  BadgeStyle.Flat,        "Hello",   "World",   "red",      "blue",       null,   null,           null,        null,                  null)]
        [InlineData(10, BadgeStyle.Plastic,     "Hello",   "World",   "red",      "blue",       null,   null,           null,        null,                  null)]
        [InlineData(11, BadgeStyle.FlatSquare,  "Hello",   "World",   "yellow",   null,         null,   null,           null,        null,                  null)]
        [InlineData(12, BadgeStyle.FlatSquare,  "Hello",   "World",   "yellow",   null,         null,   null,           null,        "http://example.com",  null)]
        [InlineData(13, BadgeStyle.FlatSquare,  "Hello",   "World",   "yellow",   null,         null,   null,           null,        "http://example.com",  "https://www.github.com")]
        [InlineData(14, BadgeStyle.FlatSquare,  "Hello",   "World",   "yellow",   null,         null,   null,           null,        "",                    null)]
        [InlineData(15, BadgeStyle.FlatSquare,  "Hello",   "World",   "red",      "blue",       null,   null,           null,        null,                  null)]
        [InlineData(16, BadgeStyle.ForTheBadge, "Hello",   "World",   "yellow",   null,         null,   null,           null,        null,                  null)]
        [InlineData(17, BadgeStyle.ForTheBadge, "Hello",   "World",   "yellow",   null,         null,   null,           null,        "http://example.com",  null)]
        [InlineData(18, BadgeStyle.ForTheBadge, "Hello",   "World",   "yellow",   null,         null,   null,           null,        "http://example.com",  "https://www.github.com")]
        [InlineData(19, BadgeStyle.ForTheBadge, "Hello",   "World",   "yellow",   null,         null,   null,           null,        "",                    null)]
        [InlineData(20, BadgeStyle.ForTheBadge, "Hello",   "World",   "red",      "blue",       null,   null,           null,        null,                  null)]
        public void MakeBadge_returns_expected_svg(int id, BadgeStyle style, string leftText, string rightText, string color, string labelColor, string logo, int? logoPosition, int? logoWidth, string link1, string link2)
        {
            var badge = Badge.MakeBadge(style, leftText, rightText, color, labelColor, logo, logoPosition, logoWidth, link1, link2);
            var writer = new ApprovalTextWriter(badge.ToString(), "svg");

            Approvals.Verify(writer, new ApprovalNamer(id), Approvals.GetReporter());            
        }   

    }
}
