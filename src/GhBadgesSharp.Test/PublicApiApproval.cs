using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using PublicApiGenerator;
using Xunit;

namespace Grynwald.GhBadgesSharp.Test
{
    [UseReporter(typeof(DiffReporter))]
    public class PublicApiApproval
    {
        [Fact]
        public void Assembly_must_not_have_unapproved_API_changes()
        {
            // ARRANGE
            var assembly = typeof(Badge).Assembly;

            // ACT
            var publicApi = ApiGenerator.GeneratePublicApi(assembly);

            // ASSERT
            var writer = new ApprovalTextWriter(publicApi);
            Approvals.Verify(writer, new UnitTestFrameworkNamer(), Approvals.GetReporter());
        }
    }
}
