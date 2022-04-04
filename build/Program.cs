using Cake.Common.Tools.DotNet.Publish;
using Cake.Common.Tools.DotNetCore.Restore;
using Cake.Frosting;

public static class Program
{
  public static int Main(string[] args)
  {
    return new CakeHost()
    .UseContext<BuildContext>()
      .Run(args);
  }
}