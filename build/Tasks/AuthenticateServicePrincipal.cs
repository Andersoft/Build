using System;
using System.Threading.Tasks;
using Cake.Frosting;
using CliWrap.Buffered;

[TaskName("Authenticate Service Principal")]
[IsDependentOn(typeof(CleanDirectories))]

public sealed class AuthenticateServicePrincipal : AsyncFrostingTask<BuildContext>
{
  public override bool ShouldRun(BuildContext context)
  {
    return context.IsValid;
  }
  public override async Task RunAsync(BuildContext context)
  {
    var result = await CliWrap.Cli.Wrap("az")
      .WithArguments(new[]
      {
        "login",
        "--service-principal",
        $"--username {context.Parameters[BuildContext.ServicePrincipalUsername]}",
        $"--password {context.Parameters[BuildContext.servicePrincipalPassword]}",
        $"--tenant {context.Parameters[BuildContext.servicePrincipalTenant]}"
      }, false)
      .ExecuteBufferedAsync();

    Console.WriteLine(result.StandardError + result.StandardOutput);
  }
}