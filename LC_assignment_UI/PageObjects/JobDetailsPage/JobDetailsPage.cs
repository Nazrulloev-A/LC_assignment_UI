using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Reqnroll;

namespace LC_assignment_UI.PageObjects.JobDetailsPage
{
    public class JobDetailsPage
    {
        private readonly IPage _page;
        private readonly IConfiguration _config;
        private readonly ScenarioContext _scenarioContext;

        public JobDetailsPage(IPage page, IConfiguration config, ScenarioContext scenarioContext)
        {
            _page = page;
            _config = config;
            _scenarioContext = scenarioContext;
        }

        private ILocator Jobtitle => _page.Locator("//h1 | //div[contains(@class,'job-title')] | //span[contains(@class,'job-title')]");
        private ILocator JobLocation => _page.Locator(".location, .job-location, .position-location");
        private ILocator JobId => _page.Locator(".au-target.jobId");
        private ILocator BackToSearchResultsLinkBtn => _page.Locator("//a[@title='Back to search results']");
        private ILocator NextJobLinkBtn => _page.Locator("//a[@title='Next job']");
        private ILocator JobDescriptionText => _page.Locator(".job-description, .description, .job-summary").Nth(1);




        public async Task<string> ConfirmJobTitle()
        {
            // Wait until the job title is visible
            await Jobtitle.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            var jobTitle = await Jobtitle.TextContentAsync() ?? string.Empty;
            // Clean up spaces (leading, trailing, and multiple inner spaces)
            var ConfirmedJobTitle = System.Text.RegularExpressions.Regex.Replace(jobTitle, @"\s+", " ").Trim();
            _scenarioContext.Add("ComJobTitle", ConfirmedJobTitle);

            // Return the cleaned job title
            return ConfirmedJobTitle;
        }

        public async Task<string> ConfirmJobLocation()
        {
            // Wait until the Job Location element is visible
            await JobLocation.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

            // Get the raw text content
            var jobLocation = await JobLocation.TextContentAsync() ?? string.Empty;

            // Clean up spaces (leading/trailing and multiple inner spaces)
            var ConfirmedJobLocation = System.Text.RegularExpressions.Regex.Replace(jobLocation, @"\s+", " ").Trim();

            var ConfirmedCity = ConfirmedJobLocation.Split(',').FirstOrDefault()?.Trim() ?? string.Empty;

            // Store the cleaned location in ScenarioContext
            _scenarioContext.Add("ComJobLocation", ConfirmedCity);

            // Return the cleaned location
            return ConfirmedCity;
        }

        public async Task<string> ConfirmJobId()
        {
            // Wait until the Job ID element is visible
            await JobId.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

            // Get the raw text content (e.g., "Job ID : 2524429")
            var jobIdText = await JobId.TextContentAsync() ?? string.Empty;

            // Extract only the numeric Job ID using Regex
            var ConfirmedJobId = System.Text.RegularExpressions.Regex.Match(jobIdText, @"\d+").Value;

            // Store the cleaned Job ID in ScenarioContext
            _scenarioContext.Add("ComJobId", ConfirmedJobId);

            // Return only the numeric Job ID
            return ConfirmedJobId;
        }

        public async Task<bool> VerifyBackToSearchResultsLinkButtonIsDisplayedAsync()
        {

            // Wait for the Back to Search Results link button to be visible
            await BackToSearchResultsLinkBtn.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await BackToSearchResultsLinkBtn.ScrollIntoViewIfNeededAsync();

            // Check if the button is visible and enabled
            bool isVisible = await BackToSearchResultsLinkBtn.IsVisibleAsync();
            bool isEnabled = await BackToSearchResultsLinkBtn.IsEnabledAsync();

            // Get the actual link text (trimmed)
            var linkText = (await BackToSearchResultsLinkBtn.InnerTextAsync() ?? string.Empty).Trim();
            var expectedTextForBackToSerchLink = "Back to search results";

            // Validate the link text
            linkText.Should().Be(expectedTextForBackToSerchLink, "because the link text should match the expected value");

            // Validate visibility and enabled state
            isVisible.Should().BeTrue("because the button should be visible on the page");
            isEnabled.Should().BeTrue("because the button should be enabled and clickable");

            // Return final result if you want to use it elsewhere (optional)
            return isVisible && isEnabled;
        }


        public async Task<bool> VerifyNextjobLinkButtonIsDisplayedAsync()
        {

            // Wait for the Back to Search Results link button to be visible
            await NextJobLinkBtn.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
            await NextJobLinkBtn.ScrollIntoViewIfNeededAsync();

            // Check if the button is visible and enabled
            bool isVisible = await NextJobLinkBtn.IsVisibleAsync();
            bool isEnabled = await NextJobLinkBtn.IsEnabledAsync();

            // Get the actual link text (trimmed)
            var linkTextJob = (await NextJobLinkBtn.InnerTextAsync() ?? string.Empty).Trim();
            var expectedTextForNextJobLink = "Next job";

            // Validate the link text
            linkTextJob.Should().Be(expectedTextForNextJobLink, "because the link text should match the expected value");

            // Validate visibility and enabled state
            isVisible.Should().BeTrue("because the button should be visible on the page");
            isEnabled.Should().BeTrue("because the button should be enabled and clickable");

            // Return final result if you want to use it elsewhere (optional)
            return isVisible && isEnabled;
        }

        public async Task VerifyJobDescriptionIsVisibleAsync()
        {
            // Wait for the Job Description element to be visible
            await JobDescriptionText.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });

            // Check visibility and enabled state
            bool isVisible = await JobDescriptionText.IsVisibleAsync();
            bool isEnabled = await JobDescriptionText.IsEnabledAsync();

            // Get text content and trim it
            var descriptionText = (await JobDescriptionText.InnerTextAsync() ?? string.Empty).Trim();

            // Assertions
            isVisible.Should().BeTrue("because the job description should be visible on the page");
            isEnabled.Should().BeTrue("because the job description element should be enabled");
            descriptionText.Should().NotBeNullOrWhiteSpace("because job description should not be empty");


        }

        public async Task ClickReturnToJobSearchButton()
        {
            await BackToSearchResultsLinkBtn.ClickAsync();

        }

    }
}