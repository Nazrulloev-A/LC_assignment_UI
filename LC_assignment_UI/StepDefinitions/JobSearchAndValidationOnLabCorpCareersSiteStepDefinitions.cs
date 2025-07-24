using LC_assignment_UI.PageObjects;
using LC_assignment_UI.PageObjects.CareerPage;
using LC_assignment_UI.PageObjects.HomePage;
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

    public JobSearchAndValidationOnLabCorpCareersSiteStepDefinitions(BasePage basePage, IPage page, HomePage homePage, CareersPage careersPage)
    {
        _page = page;
        _basePage = basePage;
        _homePage = homePage;
        _careersPage = careersPage;
    }


    [Given("I open a browser and navigate to labcorp homepage")]
    public async Task GivenIOpenABrowserAndNavigateToLabcorpHomepage()
    {
        await _basePage.Navigate();
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
        await _careersPage.SearchForJob("Automation Developer");

    }

    [Then("user select and browse to the position")]
    public async Task ThenUserSelectAndBrowseToThePosition()
    {






    }
}
