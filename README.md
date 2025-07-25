
# 🧪 LabCorp Careers Test Automation

## 📄 Project Description

This project provides automated testing for the **LabCorp Careers website** using **Microsoft Playwright** with the **Reqnroll (SpecFlow) BDD framework** in **C#**.  
It follows the **Page Object Model (POM)** for maintainable and scalable automation.  
Test scenarios are written in **Gherkin syntax**, promoting collaboration between technical and non-technical stakeholders.  
Playwright ensures **fast, reliable, and cross-browser** test execution, seamlessly integrated into CI/CD pipelines.

---

## 📂 LC_assignment_UI (Root Solution)

<img width="560" height="662" alt="Project Structure" src="https://github.com/user-attachments/assets/b79af8fa-e9af-4138-9c64-8cdc53bedaf7" />

---

## 📁 Folder Structure & Purpose

| Folder               | Purpose                                                                                                            |
|----------------------|--------------------------------------------------------------------------------------------------------------------|
| **Features**          | Contains `.feature` files written in Gherkin for BDD scenarios.                                                    |
| **Fixtures**          | Browser setup utilities, configuration models, and Playwright driver factories.                                   |
| **Hooks**             | Global hooks like `[BeforeScenario]`, `[AfterScenario]` for scenario lifecycle management.                         |
| **Models**            | Configuration and data models.                                                                                     |
| **PageObjects**       | Implements **Page Object Model (POM)**. Each page has a separate class with locators and actions.                   |
| **StepDefinitions**   | Binds Gherkin steps to C# methods that interact with Playwright.                                                   |
| **appsettings.json**  | Configuration file for base URL, browser settings, and timeouts.                                                    |
| **Startup.cs**        | Entry point for Dependency Injection and Framework Initialization.                                                  |

---

## ✨ Key Features

- **Page Object Model (POM):** Clean separation of page logic and test logic.
- **BDD with Reqnroll (SpecFlow):** Human-readable test cases using Gherkin.
- **Multiple Locator Strategies:** ID, XPath, CSS Selectors, Text, Role selectors.
- **Robust Wait Strategies:** Auto-waiting and custom explicit waits.
- **Cross-browser Support:** Chromium, Firefox, WebKit (Chrome by default).
- **Comprehensive Assertions:** Title, Location, ID, Description, Requirements.
- **Cross-page Verification:** Data consistency between search and application pages.
- **Dependency Injection:** Configured via **Startup.cs**.
- **Configuration Management:** Via `appsettings.json` for environments and browser settings.

---

## 🧪 Test Scenario Flow

1. Navigate to [www.labcorp.com](https://www.labcorp.com)
2. Click on **Careers** link.
3. Search for **"QA Test Automation Developer"**.
4. Select the **first available position**.
5. Validate job details:
   - Title
   - Location
   - Job ID
   - Description
   - Requirements
6. Click **Apply Now** and verify job details on the application page.
7. Click **Return to Job Search**.

---

## 🔍 Locator Strategies Used

| Locator Type     | Purpose                                                       |
|------------------|---------------------------------------------------------------|
| **By ID**         | Unique form elements.                                          |
| **By XPath**      | Complex DOM traversals, dynamic locators.                      |
| **By CSS Selector** | Class & attribute-based selections.                          |
| **By Text**       | Visible text elements.                                         |
| **By Role Selector** | Accessibility roles for buttons, links, and inputs.          |

---

## ⏱️ Wait Methods Implemented

| Wait Type            | Description                                                                 |
|----------------------|-----------------------------------------------------------------------------|
| **Auto-Wait (Default)**| Playwright auto-waits for elements before actions.                         |
| **Explicit Waits**     | `Locator.WaitForAsync()` for clickable, visible, or hidden elements.        |
| **Page Load Wait**     | Ensures page is fully loaded before interactions.                          |
| **Custom Wait Wrappers** | Reusable helper methods for dynamic wait conditions.                     |

---

## ▶️ Running the Tests

### Prerequisites:
- .NET SDK **7.0+**
- Playwright CLI (`playwright install`)
- Visual Studio 2022 or JetBrains Rider
- Chrome/Chromium installed (default browser)

### Execute Tests:
```bash
# Run all tests
dotnet test

# Run a specific feature
dotnet test --filter "FullyQualifiedName~JobSearch"

# Run tests in headless mode
dotnet test -- RunConfiguration.Headless=true
```

---

## ⚙️ Configuration (`appsettings.json`)

| Configuration     | Value                                 |
|-------------------|---------------------------------------|
| **BaseUrl**        | "https://www.labcorp.com"            |
| **Browser**        | "chromium"                           |
| **Viewport Size**  | 1920x1080                            |
| **Headless**       | false (set to true for CI pipelines) |
| **Timeouts**       | 30s explicit waits, 60s page load    |

---

## ✅ Assertions Performed

| Validation Item     | Description                                                          |
|---------------------|----------------------------------------------------------------------|
| **Job Title**        | Contains "QA Test Automation Developer"                             |
| **Job Location**     | Ensures location is present                                         |
| **Job ID**           | Verifies Job ID is displayed                                        |
| **Job Description**  | Validates it contains automation-related content                    |
| **Job Requirements** | Checks for tools like Playwright, Selenium                          |
| **Experience**       | Verifies experience requirements                                    |

---

## ⚠️ Error Handling

- Multiple fallback locators to handle dynamic DOM changes.
- Graceful handling of missing elements using try-catch mechanisms.

---


