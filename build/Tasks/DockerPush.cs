using Cake.Docker;
using Cake.Frosting;

[TaskName("Docker Push")]
[IsDependentOn(typeof(DockerBuild))]
public sealed class DockerPush : FrostingTask<BuildContext>
{
  public override bool ShouldRun(BuildContext context)
  {
    return context.IsValid;
  }
  public override void Run(BuildContext context)
  {
    var projectName = context.Parameters[BuildContext.ProjectName].ToLower();
    context.DockerPush($"{context.Parameters[BuildContext.DockerRegistry]}.azurecr.io/{projectName}:{context.Parameters[BuildContext.BuildVersion]}");
    context.DockerPush($"{context.Parameters[BuildContext.DockerRegistry]}.azurecr.io/{projectName}:latest");
  }
}