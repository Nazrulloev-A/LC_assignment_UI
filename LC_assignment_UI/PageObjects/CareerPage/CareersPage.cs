using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC_assignment_UI.PageObjects.CareerPage;

public class CareersPage
{
    private readonly IPage _page;
    private readonly IConfiguration _config;
    private readonly BasePage _basePage;

    public CareersPage(IPage page, IConfiguration config, BasePage basePage)
    {
        _config = config;
        _page = page;
        _basePage = basePage;
    }

    private ILocator HeaderSearchPlaceholder => _page.Locator("//input[@id='typehead']");
    private ILocator GlobalsearchButton => _page.Locator("//button[@id='ph-search-backdrop']");



    public async Task SearchForJob(string jobtitle)
    {
        // Fill the search input
        await HeaderSearchPlaceholder.FillAsync(jobtitle);

        // Validate GlobalSearchButton visibility and clickability
        try
        {
            // Wait until the button is visible in the DOM
            await GlobalsearchButton.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

            // Check if the button is enabled and visible on the page
            bool isEnabled = await GlobalsearchButton.IsEnabledAsync();
            bool isVisible = await GlobalsearchButton.IsVisibleAsync();

            if (isEnabled && isVisible)
            {
                await GlobalsearchButton.ClickAsync();
                Console.WriteLine("Global Search button clicked successfully.");
            }
            else
            {
                Console.WriteLine($"Global Search button is not clickable. Visible: {isVisible}, Enabled: {isEnabled}");
                throw new Exception("Global Search button is not in a clickable state.");
            }
        }
        catch (TimeoutException)
        {
            Console.WriteLine("Timeout: Global Search button did not become visible.");
            throw;
        }
        catch (PlaywrightException ex)
        {
            Console.WriteLine($"Playwright Exception while clicking Global Search button: {ex.Message}");
            throw;
        }
    }
}
