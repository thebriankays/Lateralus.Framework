namespace Lateralus.Framework.DependencyScanning;

public enum DependencyType
{
    Unknown,
    NuGet,
    Npm,
    PyPi,
    DockerImage,
    GitSubmodule,
    DotNetSdk,
    DotNetTargetFramework,
    GitHubActions,
}
