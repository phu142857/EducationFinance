using System.Xml.Linq;

namespace AdminPortal.API.IntegrationTests;

public sealed class ArchitectureDependencyTests
{
    [Fact]
    public void Production_projects_follow_the_approved_reference_graph()
    {
        var portalRoot = Path.Combine(FindRepositoryRoot(), "src", "AdminPortal");

        AssertReferences(portalRoot, "AdminPortal.Domain", []);
        AssertReferences(portalRoot, "AdminPortal.Application", ["AdminPortal.Domain"]);
        AssertReferences(portalRoot, "AdminPortal.Infrastructure", ["AdminPortal.Application", "AdminPortal.Domain"]);
        AssertReferences(portalRoot, "AdminPortal.API", ["AdminPortal.Application", "AdminPortal.Infrastructure"]);
    }

    [Fact]
    public void Production_projects_do_not_reference_the_other_portal()
    {
        var portalRoot = Path.Combine(FindRepositoryRoot(), "src", "AdminPortal");
        var references = Directory.GetFiles(portalRoot, "*.csproj", SearchOption.AllDirectories)
            .SelectMany(ProjectReferences)
            .ToArray();

        Assert.DoesNotContain(references, reference =>
            reference.StartsWith("EServicePortal.", StringComparison.Ordinal));
    }

    private static void AssertReferences(string portalRoot, string projectName, string[] expected)
    {
        var projectPath = Path.Combine(portalRoot, projectName, $"{projectName}.csproj");
        Assert.Equal(expected.Order(), ProjectReferences(projectPath).Order());
    }

    private static IEnumerable<string> ProjectReferences(string projectPath) =>
        XDocument.Load(projectPath)
            .Descendants("ProjectReference")
            .Select(reference => Path.GetFileNameWithoutExtension(reference.Attribute("Include")!.Value));

    private static string FindRepositoryRoot()
    {
        for (var directory = new DirectoryInfo(AppContext.BaseDirectory); directory is not null; directory = directory.Parent)
        {
            if (File.Exists(Path.Combine(directory.FullName, "EducationFinance.slnx")))
            {
                return directory.FullName;
            }
        }

        throw new DirectoryNotFoundException("Could not locate the EducationFinance repository root.");
    }
}
