Feature: Job Search and Validation on LabCorp Careers Site

  As a job seeker
  I want to search for a specific job on the LabCorp Careers site
  So that I can validate job details before applying

  Scenario: Navigate to Careers and search for a QA position
    Given I open a browser and navigate to labcorp homepage
    When I find and click on the Careers link
    And I search for any position that is active 
    Then user select and browse to the position

