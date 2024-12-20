namespace WiseReminder.GenericUnitTests;

public class SealedModificatorTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void AllClassesShouldBeSealedOrAbstract()
    {
        var referenceTypes = new[]
        {
            typeof(Date),
            typeof(ICacheService),
            typeof(CacheService),
            typeof(BaseCategoryRequest)
        };

        var types = referenceTypes
            .Select(t => t.Assembly)
            .Distinct()
            .SelectMany(a => a.GetTypes())
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