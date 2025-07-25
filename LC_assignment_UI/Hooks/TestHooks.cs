﻿using LC_assignment_UI.PageObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using Reqnroll;
using Reqnroll.BoDi;
using Reqnroll.Tracing;
using System.Reflection;

namespace LC_assignment_UI.Hooks;


[Binding]
public class TestHooks
{
    private readonly BasePage _basePage;

    public TestHooks(BasePage basePage)
    {
        _basePage = basePage;
    }

    public Fixtures.BrowserModel.WebDrivers.WebDriverFixture webDriverFixture;

    [BeforeScenario]
    public async Task CreateConfig(IObjectContainer container, ScenarioContext context)
    {
        Console.WriteLine("****** Test Hook - BeforeScenario - Started -" + context.ScenarioInfo.Title);

        if (_basePage.Config is null)
        {
            _basePage.Config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .AddUserSecrets(typeof(TestHooks).GetTypeInfo().Assembly)
                .AddEnvironmentVariables()
                .Build();
        }

        container.RegisterInstanceAs(_basePage.Config);

        webDriverFixture = new Fixtures.BrowserModel.WebDrivers.WebDriverFixture(_basePage.Config);
        await webDriverFixture.InitializeAsync();
        _basePage.Browser = webDriverFixture.Browser;

        
        _basePage.BrowserContext = await _basePage.Browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = new ViewportSize
            {
                Width = 1920,
                Height = 1080

            }
        });

        _basePage.Page = await _basePage.BrowserContext.NewPageAsync();
        _basePage.Page.SetDefaultTimeout(60000);


        container.RegisterInstanceAs(_basePage.Page);
        container.RegisterInstanceAs(_basePage.Browser);

        Console.WriteLine("****** Test Hook - BeforeScenario - End");
    }

    [AfterScenario]
    public async Task AfterScenario(IObjectContainer container, ScenarioContext context)
    {
        Console.WriteLine("****** Test Hook - AfterScenario - Started_" + context.ScenarioInfo.Title);

        string fileNameBase = string.Format("Fail_{0}", context.ScenarioInfo.Title.ToIdentifier());
        var browser = _basePage.Browser;
        var page = _basePage.Page;

        if (context.TestError != null)
        {
            await page.ScreenshotAsync(new()
            {
                Path = fileNameBase + ".png",
                FullPage = true,
            });
        }

        await browser.CloseAsync();
        await browser.DisposeAsync();

        Console.WriteLine("****** Test Hook - AfterScenario - End");
    }
}