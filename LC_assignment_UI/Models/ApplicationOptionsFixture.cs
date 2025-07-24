using Microsoft.Extensions.Configuration;

namespace LC_assignment_UI.Models;

public class ApplicationOptionsFixture
{
    public ApplicationOptionsFixture(IConfiguration configuration)
    {
        Options = ApplicationOptions.GetConfig(configuration);
    }

    public ApplicationOptions Options { get; private set; }
}
