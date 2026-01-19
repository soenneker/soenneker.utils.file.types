using Soenneker.Utils.File.Types.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Utils.File.Types.Tests;

[Collection("Collection")]
public class FileTypeUtilTests : FixturedUnitTest
{
    private readonly IFileTypeUtil _util;

    public FileTypeUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IFileTypeUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
