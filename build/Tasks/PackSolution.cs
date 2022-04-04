using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Pack;
using Cake.Core;
using Cake.Frosting;

[TaskName("Pack Solution")]
[IsDependentOn(typeof(DockerPush))]

public sealed class PackSolution : FrostingTask<BuildContext>
{
  public override bool ShouldRun(BuildContext context)
  {
    return context.IsValid;
  }
  public override void Run(BuildContext context)
  {
    context.DotNetPack($"../../{context.Parameters[BuildContext.ProjectName]}.sln", new DotNetPackSettings
    {
      Configuration = "Release",
      OutputDirectory = "./artifacts/consumeables",
      Runtime = "linux-x64",
      ArgumentCustomization = (builder) => builder.Append($"/p:Version={context.Parameters[BuildContext.BuildVersion]}")
    });
  }
}