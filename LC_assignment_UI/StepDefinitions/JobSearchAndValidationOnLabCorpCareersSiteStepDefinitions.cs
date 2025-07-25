using FluentAssertions;
using LC_assignment_UI.Models;
using LC_assignment_UI.PageObjects;
using LC_assignment_UI.PageObjects.CareerPage;
using LC_assignment_UI.PageObjects.HomePage;
using LC_assignment_UI.PageObjects.JobDetailsPage;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Reqnroll;

namespace LC_assignment_UI.StepDefinitions;

[Binding]
public class JobSearchAndValidationOnLabCorpCareersSiteStepDefinitions
{
    private readonly IPage _page;
    private readonly BasePage _basePage;
    private readonly HomePage _homePage;
    private readonly CareersPage _careersPage;
    private readonly JobDetailsPage _jobDetailsPage;
    private readonly IConfiguration _config;
    private readonly ScenarioContext _scenarioContext;


    public JobSearchAndValidationOnLabCorpCareersSiteStepDefinitions(BasePage basePage, IPage page, HomePage homePage, 
        CareersPage careersPage ,JobDetailsPage jobDetailsPage,IConfiguration config,ScenarioContext scenarioContext)
    {
        _page = page;
        _basePage = basePage;
        _homePage = homePage;
        _careersPage = careersPage;
        _jobDetailsPage = jobDetailsPage;
        _config = config;
        _scenarioContext = scenarioContext;
    }


    [Given("I open a browser and navigate to labcorp homepage")]
    public async Task GivenIOpenABrowserAndNavigateToLabcorpHomepage()
    {
        await _basePage.Navigate();
        //Fluent Assertions 
        _basePage.Page.Url.Should().Contain("labcorp.com")
        .And.Be(ApplicationOptions.GetConfig(_config).BaseUrl, "because it should match the homepage URL exactly");
    }

    [When("I find and click on the Careers link")]
    public async Task WhenIFindAndClickOnTheCareersLink()
    {
        await _homePage.VerifyCareersLinkIsClickableAsync();
        await _homePage.ClickOnCareersLink();

    }

    [When("I search for any position that is active")]
    public async Task WhenISearchForAnyPositionThatIsActive()
    {
        _basePage.Page.Url.Should().Contain("career");
        await _careersPage.SearchForJob("Automation Developer");
        await _careersPage.ValidateSearchResultIsNotEmptyAsync();
        
    }

    [Then("user select and browse to the position")]
    public async Task ThenUserSelectAndBrowseToThePosition()
    {
        await _careersPage.BrowseThePossition();
        _basePage.Page.Url.Should().Contain("job");
    }

    [Then("user should verify the job title contains selected position")]
    public async Task ThenUserShouldVerifyTheJobTitleContainsSelectedPosition()
    {
        var actualJobTitle = await _jobDetailsPage.ConfirmJobTitle();
        var expectedJobTitle = _scenarioContext.Get<string>("ComJobTitle");
        actualJobTitle.Should().Contain(expectedJobTitle, "because the job title should match the selected position");

    }

    [Then("user should verify the job location is displayed")]
    public async Task ThenUserShouldVerifyTheJobLocationIsDisplayed()
    {
        var actuallocation = await _jobDetailsPage.ConfirmJobLocation();
        var expectedLocation = _scenarioContext.Get<string>("ComJobLocation");
        expectedLocation.Should().Contain(expectedLocation, "because the job location should match the selected position");
    }

    [Then("user should verify the job ID is displayed")]
    public async Task ThenUserShouldVerifyTheJobIDIsDisplayed()
    {
       var actualJobId = await _jobDetailsPage.ConfirmJobId();
       var expectedJobId = _scenarioContext.Get<string>("ComJobId");
       expectedJobId.Should().Contain(expectedJobId, "because the jobId should match the selected position");

    }

    // Any 3 other assertions of choice

    [Then("user should verify the Back to serch results link button is displayed")]
    public async Task ThenUserShouldVerifyTheBackToSerchResultsLinkButtonIsDisplayed()
    {
       await _jobDetailsPage.VerifyBackToSearchResultsLinkButtonIsDisplayedAsync();
    }

    [Then("user shouldverify the Back to serch results link button is displayed")]
    public async Task ThenUserShouldverifyTheBackToSerchResultsLinkButtonIsDisplayed()
    {
        await _jobDetailsPage.VerifyNextjobLinkButtonIsDisplayedAsync();
    }

    [Then("user should verify the job description are listed")]
    public async Task  ThenUserShouldVerifyTheJobDescriptionAreListed()
    {
        await _jobDetailsPage.VerifyJobDescriptionIsVisibleAsync();
    }

    [When("user click Return to Job Search")]
    public async Task WhenUserClickReturnToJobSearch()
    {
        await _jobDetailsPage.ClickReturnToJobSearchButton();
    }

    [When("user should be redirected to the job search page")]
    public void WhenUserShouldBeRedirectedToTheJobSearchPage()
    {
        _basePage.Page.Url.Should().Contain("career");
    }

}

