Feature: Get Hello Error500

  Verify that the Hello Error-500 page works

  Scenario: Access the Hello Error-500 page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/hello/error-500"
    Then the response status code should be 500
    And I should see "Internal server error" in the response
    And I should see "The website tripped over something internal" in the response
