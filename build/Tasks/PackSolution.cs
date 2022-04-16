using System;
using System.Threading.Tasks;
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Pack;
using Cake.Core;
using Cake.Frosting;
using CliWrap;
using CliWrap.Buffered;

[TaskName("Pack Solution")]
[IsDependentOn(typeof(DockerPush))]

public sealed class PackSolution : AsyncFrostingTask<BuildContext>
{
  public override bool ShouldRun(BuildContext context)
  {
    return context.IsValid;
  }
  public override async Task RunAsync(BuildContext context)
  {
    Environment.SetEnvironmentVariable("", $"{{\"endpointCredentials\": [{{\"endpoint\":\"{context.Parameters[BuildContext.FeedUrl]}\", \"username\":\"docker\", \"password\":\"{context.Parameters[BuildContext.FeedAccessToken]}\"}}]}}");
    
    context.DotNetPack($"../../{context.Parameters[BuildContext.ProjectName]}.sln", new DotNetPackSettings
    {
      Configuration = "Release",
      OutputDirectory = "./artifacts/consumeables",
      Runtime = "linux-x64",
      ArgumentCustomization = (builder) => builder.Append($"/p:Version={context.Parameters[BuildContext.BuildVersion]}")
    });
  }
}