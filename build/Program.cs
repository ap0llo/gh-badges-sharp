using System.Collections.Generic;
using Cake.AzurePipelines.Module;
using Cake.Core;
using Cake.DotNetLocalTools.Module;
using Cake.Frosting;
using Grynwald.SharedBuild;

return new CakeHost()
    .UseModule<AzurePipelinesModule>()
    .UseModule<LocalToolsModule>()
    .InstallToolsFromManifest(".config/dotnet-tools.json")
    .UseSharedBuild<BuildContext>()
    .Run(args);


public class BuildContext : DefaultBuildContext
{
    public override IReadOnlyCollection<IPushTarget> PushTargets { get; } = new[]
    {
         new PushTarget(
            type: PushTargetType.MyGet,
            feedUrl: "https://www.myget.org/F/ap0llo-gh-badges-sharp/api/v3/index.json",
            isActive: context => context.Git.IsMasterBranch || context.Git.IsReleaseBranch
         ),
         KnownPushTargets.NuGetOrg(isActive: context => context.Git.IsReleaseBranch)
     };

    public BuildContext(ICakeContext context) : base(context)
    { }
}
