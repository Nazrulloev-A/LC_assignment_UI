Feature: Job Search and Validation on LabCorp Careers Site

  As a job seeker
  I want to search for a specific job on the LabCorp Careers site
  So that I can validate job details before applying

  Scenario: Navigate to Careers and search for a QA position
    Given I open a browser and navigate to labcorp homepage
    When I find and click on the Careers link
    And I search for any position that is active 
    Then user select and browse to the position
	And user should verify the job title contains selected position
    And user should verify the job location is displayed
    And user should verify the job ID is displayed
	And user should verify the Back to serch results link button is displayed
    And user should verify the Back to serch results link button is displayed
    And user should verify the job description are listed
    When user click Return to Job Search
    And user should be redirected to the job search page
   

