using System.IO;
using Cake.Frosting;

[TaskName("Clean Directory")]
public sealed class CleanDirectories : FrostingTask<BuildContext>
{
  public override bool ShouldRun(BuildContext context)
  {
    return context.IsValid;
  }

  public override void Run(BuildContext context)
  {
    if (Directory.Exists("../artifacts"))
    {
      Directory.Delete("../artifacts", true);
    }
  }
}