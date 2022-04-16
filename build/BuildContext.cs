using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Frosting;

public class BuildContext : FrostingContext
{
  public IReadOnlyDictionary<string, string> Parameters;
  public bool IsValid = true;
  public const string FeedAccessToken = "feedAccessToken",
    BuildVersion = "buildVersion",
    ServicePrincipalUsername = "servicePrincipalUsername",
    servicePrincipalTenant = "servicePrincipalTenant",
    servicePrincipalPassword = "servicePrincipalPassword",
    DockerRegistry = "dockerRegistry",
    ProjectName  = "projectName",
    FeedUrl = "feedUrl",
    FeedSource = "feedSource" ;

  public BuildContext(ICakeContext context) : base(context)
  {
    var arguments = new Dictionary<string, string>();
    foreach (var argument in new string[] { FeedAccessToken, FeedUrl, BuildVersion, ServicePrincipalUsername, servicePrincipalTenant, servicePrincipalPassword, DockerRegistry, ProjectName, FeedSource })
    {
      if (context.Arguments.HasArgument(argument))
      {
        arguments[argument] = context.Arguments.GetArgument(argument);
      }
      else
      {
        context.Log.Error("Missing argument: {0}", argument);
        IsValid = false;
      }
    }
    Parameters = arguments;
  }
}