using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_assignment_UI.Fixtures.BrowserModel;

public class BrowserOptions
{
    public const string SectionName = "BrowserOptions";
    public string BrowserType { get; set; }
    public bool Headless { get; set; }
    public int SlowMo { get; set; }

    public static BrowserOptions GetConfig(IConfiguration configuration)
    {
        var browserOptions = new BrowserOptions();
        configuration.GetSection(SectionName).Bind(browserOptions);
        return browserOptions;
    }
}
