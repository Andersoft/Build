using System.IO;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.NuGet.Push;
using Cake.Frosting;

[TaskName("Nuget Push")]
[IsDependentOn(typeof(PackSolution))]
public sealed class NugetPush : FrostingTask<BuildContext>
{
  public override bool ShouldRun(BuildContext context)
  {
    return context.IsValid;
  }
  public override void Run(BuildContext context)
  {
    var packages = Directory.EnumerateFiles("./artifacts/consumeables", "*.nupkg");
    foreach (var package in packages)
    {
      context.DotNetNuGetPush(package, new DotNetNuGetPushSettings
      {
        Source = context.Parameters[BuildContext.FeedSource],
        ApiKey = context.Parameters[BuildContext.FeedAccessToken],
        SkipDuplicate = true
      });
    }
  }
}