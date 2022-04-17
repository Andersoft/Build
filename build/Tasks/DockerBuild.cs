using System;
using System.Threading.Tasks;
using Cake.Docker;
using Cake.Frosting;
using CliWrap.Buffered;

[TaskName("Docker Build")]
[IsDependentOn(typeof(DockerAuthenticate))]
public sealed class DockerBuild : FrostingTask<BuildContext>
{
  public override bool ShouldRun(BuildContext context)
  {
    var projectName = context.Parameters[BuildContext.ProjectName].ToLower();
    bool shouldBuild = false;
    try
    {
      var response = CliWrap.Cli.Wrap("docker")
        .WithArguments(new[]
        {
          "manifest",
          "inspect",
          $"{context.Parameters[BuildContext.DockerRegistry]}.azurecr.io/{projectName}:{context.Parameters[BuildContext.BuildVersion]}"
        })
        .ExecuteBufferedAsync()
        .GetAwaiter();
      var result = response.GetResult();
    }
    catch (Exception e)
    {
      shouldBuild = true;
    }

    return context.IsValid && shouldBuild;
  }
  public override void Run(BuildContext context)
  {
    var projectName = context.Parameters[BuildContext.ProjectName].ToLower();
    
    context.DockerBuild(new DockerImageBuildSettings
    {
      BuildArg = new[]
      {
        $"FEED_ACCESSTOKEN={context.Parameters[BuildContext.FeedAccessToken]}",
      },
      Tag = new[]
      {
        $"{projectName}:latest",
        $"{projectName}:{context.Parameters[BuildContext.BuildVersion]}",
        $"{context.Parameters[BuildContext.DockerRegistry]}.azurecr.io/{projectName}:{context.Parameters[BuildContext.BuildVersion]}",
        $"{context.Parameters[BuildContext.DockerRegistry]}.azurecr.io/{projectName}:latest",
      },

    }, "../../");
  }
}