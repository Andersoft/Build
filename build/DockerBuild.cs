using Cake.Docker;
using Cake.Frosting;

[TaskName("Docker Build")]
[IsDependentOn(typeof(DockerAuthenticate))]
public sealed class DockerBuild : FrostingTask<BuildContext>
{
  public override bool ShouldRun(BuildContext context)
  {
    return context.IsValid;
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