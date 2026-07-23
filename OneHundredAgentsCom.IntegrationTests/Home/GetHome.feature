Feature: Get Home

  Verify that the home page works

  Scenario: Access the Home page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request the home page
    Then the response status code should be 200
    And I should see "100" in the response
    And I should see "This project is what happens" in the response
    And I should see "Here they are" in the response
    And I should see "/travel-landmark-agent" as a hyperlink location
    And I should see "Travel landmark agent" in the response
