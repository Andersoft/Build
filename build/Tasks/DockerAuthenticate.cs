using System;
using System.Threading.Tasks;
using Cake.Frosting;
using CliWrap.Buffered;

[TaskName("Docker Authenticate")]
[IsDependentOn(typeof(AuthenticateServicePrincipal))]
public sealed class DockerAuthenticate : AsyncFrostingTask<BuildContext>
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
        "acr",
        "login",
        $"--name {context.Parameters[BuildContext.DockerRegistry]}"
      }, false)
      .ExecuteBufferedAsync();

    Console.WriteLine(result.StandardError + result.StandardOutput);
  }
}