using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using System;
using System.Threading.Tasks;

namespace LC_assignment_UI.PageObjects.HomePage
{
    public class HomePage
    {
        private readonly IPage _page;
        private readonly IConfiguration _config;
        private readonly BasePage _basePage;

        public HomePage(IPage page, IConfiguration config,BasePage basePage)
        {
            _config = config;
            _page = page;
            _basePage = basePage;
        }

        private ILocator CareersLink => _page.Locator("//a[contains(text(), 'Careers') and contains(@href, 'careers')]").Nth(1);
       

        public async Task ClickOnCareersLink()
        {
            await CareersLink.ClickAsync();
            _basePage.Page.Dialog += async (_, dialog) =>
            {
                await dialog.DismissAsync();
            };
        }

        public async Task<bool> VerifyCareersLinkIsClickableAsync()
        {
            try
            {
                // Wait for the link to be visible and enabled (clickable)
                await CareersLink.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Visible,
                    Timeout = 5000
                });

                // Get the link text
                var linkText = await CareersLink.InnerTextAsync();

                // Validate link text equals "Careers"
                if (linkText.Trim().Equals("Careers", StringComparison.OrdinalIgnoreCase))
                {
                    // Check if element is enabled and visible (clickable)
                    bool isEnabled = await CareersLink.IsEnabledAsync();
                    bool isVisible = await CareersLink.IsVisibleAsync();

                    return isEnabled && isVisible;
                }

                // Text mismatch
                return false;
            }
            catch (TimeoutException tex)
            {
                Console.WriteLine($"Timeout waiting for Careers link: {tex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating Careers link: {ex.Message}");
                return false;
            }

           
        }

       















    }
}
