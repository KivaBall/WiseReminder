namespace WiseReminder.ArchitectureTests;

public class SealedModificatorTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void PresentationClassesShouldBeSealedOrAbstract()
    {
        ValidateClasses(typeof(PresentationExtensions));
    }

    [Fact]
    public void ApplicationClassesShouldBeSealedOrAbstract()
    {
        ValidateClasses(typeof(ApplicationExtensions));
    }

    [Fact]
    public void EntityClassesShouldBeSealedOrAbstract()
    {
        ValidateClasses(typeof(Entity<>));
    }

    [Fact]
    public void InfrastructureClassesShouldBeSealedOrAbstract()
    {
        ValidateClasses(typeof(InfrastructureExtensions));
    }

    private void ValidateClasses(Type referenceType)
    {
        var types = referenceType.Assembly
            .GetTypes()
            .Where(t => t.IsClass)
            .ToList();

        foreach (var type in types)
        {
            var derivedTypes = types.Where(t => t.BaseType == type).ToList();
            var hasChildren = derivedTypes.Any();

            if (type is { IsSealed: false, IsAbstract: false } && !hasChildren)
            {
                Assert.Fail($"Class {type.FullName} must be sealed, abstract, or have children");
            }

            if (type is { IsSealed: false, IsAbstract: false } && hasChildren)
            {
                testOutputHelper.WriteLine(
                    $"Class {type.FullName} is neither sealed nor abstract but has children");

                continue;
            }

            Assert.True(type.IsSealed || type.IsAbstract,
                $"Class {type.FullName} must be sealed or abstract");
        }
    }
}