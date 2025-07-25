using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Playwright;
using Reqnroll;
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
    private readonly ScenarioContext _scenarioContext;

    public CareersPage(IPage page, IConfiguration config, BasePage basePage, ScenarioContext scenarioContext)
    {
        _config = config;
        _page = page;
        _basePage = basePage;
        _scenarioContext = scenarioContext;
    }

    private ILocator HeaderSearchPlaceholder => _page.Locator("//input[@id='typehead']");
    private ILocator GlobalsearchButton => _page.Locator("//button[@id='ph-search-backdrop']");
    private ILocator SearchResult => _page.Locator("//div[@class='phs-results-actions']");

    private ILocator Jobtitle => _page.Locator("//h1 | //div[contains(@class,'job-title')] | //span[contains(@class,'job-title')]").Nth(0);



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
               
            }
            else
            {
               
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



    public async Task ValidateSearchResultIsNotEmptyAsync()
    {
        // Wait for the SearchResult element to be visible (timeout as needed)
        await SearchResult.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

        // Ensure at least one element is present
        var count = await SearchResult.CountAsync();
        count.Should().BeGreaterThan(0, "because search results should be present");

        // Get the text content of the search result
        var resultText = await SearchResult.InnerTextAsync();

        // Validate that the result text is not null or empty
        resultText.Should().NotBeNullOrWhiteSpace("because search result should contain some text");

        
    }

    public async Task BrowseThePossition()
    {
        var jobTitle = await Jobtitle.TextContentAsync();
        var cleanedJobTitle = System.Text.RegularExpressions.Regex.Replace(jobTitle ?? string.Empty, @"\s+", " ").Trim();
        _scenarioContext.Add("JobTitle", cleanedJobTitle);
        await Jobtitle.ClickAsync();




    }








}
