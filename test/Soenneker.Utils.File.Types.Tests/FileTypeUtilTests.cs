using Soenneker.Utils.File.Types.Abstract;
using Soenneker.Tests.HostedUnit;


namespace Soenneker.Utils.File.Types.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class FileTypeUtilTests : HostedUnitTest
{
    private readonly IFileTypeUtil _util;

    public FileTypeUtilTests(Host host) : base(host)
    {
        _util = Resolve<IFileTypeUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
