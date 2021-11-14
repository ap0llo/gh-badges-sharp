using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using Xunit;

namespace Grynwald.GhBadgesSharp.Test
{
    [UseReporter(typeof(DiffReporter))]
    public partial class BadgeTest
    {
        private class ApprovalNamer : UnitTestFrameworkNamer
        {
            private readonly string m_Id;

            public override string Name => $"{base.Name}_{m_Id}";

            public ApprovalNamer(int id) : this(id.ToString("000"))
            { }

            public ApprovalNamer(string id)
            {
                m_Id = id;
            }

            public override string Subdirectory => Path.Combine(base.Subdirectory, "testdata");
        }


        [Theory]
        [PairwiseData]
        public void MakeBadge_returns_expected_svg(
            BadgeStyle style,
            [CombinatorialValues("Hello")] string leftText,
            [CombinatorialValues("World")] string rightText,
            [CombinatorialValues("yellow", "red")] string color,
            [CombinatorialValues(null, "", "blue")] string labelColor,
            [CombinatorialValues(null, "http://example.com")] string leftLink,
            [CombinatorialValues(null, "http://github.com")] string rightLink)
        {
            var testId = GetTestCaseFileName(style, leftText, rightText, color, labelColor, leftLink, rightLink);

            var leftUri = leftLink != null ? new Uri(leftLink) : null;
            var rightUri = rightLink != null ? new Uri(rightLink) : null;

            var badge = Badge.MakeBadge(style, leftText, rightText, color, labelColor, leftUri, rightUri);
            var writer = new ApprovalTextWriter(GetBadgeHtml(badge, style, leftText, rightText, color, labelColor, leftLink, rightLink), "html");

            Approvals.Verify(writer, new ApprovalNamer(testId), Approvals.GetReporter());
        }


        private static string GetTestCaseFileName(BadgeStyle style, string leftText, string rightText, string color, string labelColor, string leftLink, string rightLink)
        {
            string? GetSha256Hash(string value)
            {
                if (value == null)
                {
                    return null;
                }

                var crypt = SHA256.Create();
                var hash = crypt.ComputeHash(Encoding.UTF8.GetBytes(value));

                var result = new StringBuilder();

                foreach (var b in hash)
                {
                    result.Append(b.ToString("x2"));
                }
                return result.ToString();
            }


            var nameBuilder = new StringBuilder();

            nameBuilder.Append(style);
            nameBuilder.Append("_");
            nameBuilder.Append(leftText ?? "[null]");
            nameBuilder.Append("_");
            nameBuilder.Append(rightText ?? "[null]");
            nameBuilder.Append("_");
            nameBuilder.Append(color ?? "[null]");
            nameBuilder.Append("_");
            nameBuilder.Append(labelColor ?? "[null]");
            nameBuilder.Append("_");

            // Links are urls and most likely not a valid in a file name
            // also, the full hash might result in long file names, so only the first 5 characters
            // are included in the name which should be sufficient to make file names unique in
            // the context of this test
            nameBuilder.Append(GetSha256Hash(leftLink)?.Substring(0, 5) ?? "[null]");
            nameBuilder.Append("_");
            nameBuilder.Append(GetSha256Hash(rightLink)?.Substring(0, 5) ?? "[null]");

            return nameBuilder.ToString();
        }

        private static string GetBadgeHtml(XElement badge, BadgeStyle style, string leftText, string rightText, string color, string labelColor, string? leftLink, string? rightLink)
        {
            return $@"<!DOCTYPE html>
            <html>
            <head>
                <style>
                body {{
                    font-family: Arial, Helvetica, sans-serif;
                    font-size: 80%;
                }}
                h1 {{
                    font-size: 14pt;
                }}
                thead {{
                    font-weight: bold
                }}
                table {{
                    border-collapse: collapse;
                }}
                table, th, td {{
                    border: 1px solid black;
                }}
                td {{
                    padding: 0 10px 0 5px;
                }}
                </style>
            </head>

            <body>

            <h1>Rendered Badge:</h1>
            {badge}
            <h1>Parameters:</h1>          
              <table>
                <thead>
                  <tr> <td>Name</td> <td>Value</td> </tr>
                </thead>
                <tbody>
                  <tr> <td>Style</td> <td>{style}</td> </tr>
                  <tr> <td>LeftText</td> <td>{leftText}</td> </tr>
                  <tr> <td>RightText</td> <td>{rightText}</td> </tr>
                  <tr> <td>Color</td> <td>{color}</td> </tr>
                  <tr> <td>LabelColor</td> <td>{labelColor}</td> </tr>
                  <tr> <td>LeftLink</td> <td>{leftLink}</td> </tr>
                  <tr> <td>RightLink</td> <td>{rightLink}</td> </tr>
                </tbody>
              </table>

            </body>
            </html> ";
        }

    }
}
