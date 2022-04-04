using System.Threading.Tasks;
using Cake.Frosting;
using CliWrap.Buffered;

[TaskName("Logout Service Principal")]
[IsDependentOn(typeof(NugetPush))]
public class LogOutServicePrincipal : AsyncFrostingTask<BuildContext>
{
  public override Task RunAsync(BuildContext context)
  {
    var arguments = new[]
    {
      "logout",
      $"--username {context.Parameters[BuildContext.ServicePrincipalUsername]}"
    };

    return CliWrap.Cli.Wrap("az")
      .WithArguments(arguments, false)
      .ExecuteBufferedAsync();
  }
}