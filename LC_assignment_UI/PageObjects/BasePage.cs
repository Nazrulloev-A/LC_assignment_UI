using LC_assignment_UI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;

namespace LC_assignment_UI.PageObjects;

public class BasePage
{
    public string PageUrl { get; }
    public IPage Page { get; set; }
    public IBrowser Browser { get; set; }
    public IConfiguration Config { get; set; }
    public IBrowserContext BrowserContext { get; set; }

    public async Task Navigate()
    {
        Console.WriteLine("****** Navigation method here");
        await Page.GotoAsync(ApplicationOptions.GetConfig(Config).BaseUrl);
        Console.WriteLine("****** Navigation method - Navigated to base url");
    }
  

}
