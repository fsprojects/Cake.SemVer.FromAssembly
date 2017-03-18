var rootDirectoryPath         = MakeAbsolute(Context.Environment.WorkingDirectory);
var solutionFilePath          = "./Source/Cake.SynVer.sln";

#tool nuget:?package=xunit.runner.console

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

Task("NuGet-Package-Restore")
    .Does(() =>
{
    NuGetRestore(solutionFilePath);
});

Task("Build")
    .IsDependentOn("NuGet-Package-Restore")
    .Does(() =>
{
    MSBuild(solutionFilePath, new MSBuildSettings()
        .SetConfiguration(configuration)
        .WithProperty("Windows", "True")
        .WithProperty("TreatWarningsAsErrors", "False")
        .UseToolVersion(MSBuildToolVersion.VS2015)
        .SetVerbosity(Verbosity.Minimal)
        .SetNodeReuse(false));
});

Task("Clean")
    .Does(() =>
{
    CleanDirectories(new[] { "./BuildArtifacts/TestResults", "./BuildArtifacts/nuget" });
});

Task("Run-xUnit-Tests")
    .IsDependentOn("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    XUnit2("./Source/**/bin/" + configuration + "/*Tests.dll", new XUnit2Settings {
        OutputDirectory = "./BuildArtifacts/TestResults",
        XmlReportV1 = true,
        NoAppDomain = true
    });
});


Task("Package")
    .IsDependentOn("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    var nuGetPackSettings   = new NuGetPackSettings {
        Id                      = "Cake.SynVer",
        Version                 = "0.0.1",
        Title                   = "Cake addin to use SynVer",
        Authors                 = new[] {"Oskar Gewalli", "SPISE MISU ApS (Ramón Soto Mathiesen)"},
        Owners                  = new[] {"Oskar Gewalli", "SPISE MISU ApS (Ramón Soto Mathiesen)"},
        Description             = "Cake addin in order to be able to get next syntactic (semantic) version of nuget package based on changes to the public API",
        ProjectUrl              = new Uri("https://github.com/fsprojects/Cake.SynVer"),
        LicenseUrl              = new Uri("https://raw.githubusercontent.com/fsprojects/Cake.SynVer/master/LICENSE"),
        Copyright               = "fsprojects 2017",
        ReleaseNotes            = new string[]{"Initial release"},
        Tags                    = new [] {"semver", "synver", "Cake", "syntactic-versioning", "semantic-versioning"},
        RequireLicenseAcceptance= false,
        Symbols                 = true,
        NoPackageAnalysis       = true,
        Files                   = new [] {
            new NuSpecContent {Source = "Cake.SynVer.dll", Target = "/"},
            new NuSpecContent {Source = "Cake.SynVer.XML", Target = "/"},
        },
        Dependencies            = new List<NuSpecDependency>
        { 
            new NuSpecDependency { Id= "SynVer", Version="0.0.6"} 
        },
        BasePath                = "./Source/Cake.SynVer/bin/" + configuration,
        OutputDirectory         = "./BuildArtifacts/nuget"
    };

    NuGetPack(nuGetPackSettings);
});

Task("Default")
  .IsDependentOn("Run-xUnit-Tests");

RunTarget(target);
