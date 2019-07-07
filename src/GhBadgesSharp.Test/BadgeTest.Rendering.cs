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
        //          id  template        leftText   rightText  color       labelColor    logo    logoPosition    logoWidth    link1                  link2
        [InlineData(1,  "flat",         "Hello",   "World",   "yellow",   null,         null,   null,           null,        null,                  null)]
        [InlineData(2,  "flat",         "Hello",   "World",   "yellow",   null,         null,   null,           null,        "http://example.com",  null)]
        [InlineData(3,  "plastic",      "Hello",   "World",   "yellow",   null,         null,   null,           null,        null,                  null)]
        [InlineData(4,  "plastic",      "Hello",   "World",   "yellow",   null,         null,   null,           null,        "http://example.com",  null)]
        [InlineData(5,  "flat",         "Hello",   "World",   "yellow",   null,         null,   null,           null,        "http://example.com",  "https://www.github.com")]
        [InlineData(6,  "plastic",      "Hello",   "World",   "yellow",   null,         null,   null,           null,        "http://example.com",  "https://www.github.com")]
        [InlineData(7,  "plastic",      "Hello",   "World",   "yellow",   null,         null,   null,           null,        "",                    null)]
        [InlineData(8,  "flat",         "Hello",   "World",   "yellow",   null,         null,   null,           null,        "",                    null)]
        [InlineData(9,  "flat",         "Hello",   "World",   "red",      "blue",       null,   null,           null,        null,                  null)]
        [InlineData(10, "plastic",      "Hello",   "World",   "red",      "blue",       null,   null,           null,        null,                  null)]
        [InlineData(11,  "flat-square", "Hello",   "World",   "yellow",   null,         null,   null,           null,        null,                  null)]
        [InlineData(12,  "flat-square", "Hello",   "World",   "yellow",   null,         null,   null,           null,        "http://example.com",  null)]
        [InlineData(13,  "flat-square", "Hello",   "World",   "yellow",   null,         null,   null,           null,        "http://example.com",  "https://www.github.com")]
        [InlineData(14,  "flat-square", "Hello",   "World",   "yellow",   null,         null,   null,           null,        "",                    null)]
        [InlineData(15,  "flat-square", "Hello",   "World",   "red",      "blue",       null,   null,           null,        null,                  null)]
        public void MakeBadge_returns_expected_svg(int id, string template, string leftText, string rightText, string color, string labelColor, string logo, int? logoPosition, int? logoWidth, string link1, string link2)
        {
            // ARRANGE
            var links = new[] { link1, link2 }.Where(x => x != null);
            
            // ACT
            var badge = Badge.MakeBadge(template, leftText, rightText, color, labelColor, logo, logoPosition, logoWidth, links);
            var writer = new ApprovalTextWriter(badge.ToString(), "svg");

            // ASSERT
            Approvals.Verify(writer, new ApprovalNamer(id), Approvals.GetReporter());            
        }   

    }
}
