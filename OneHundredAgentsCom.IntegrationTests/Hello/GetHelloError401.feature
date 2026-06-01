Feature: Get Hello Error401

  Verify that the Hello Error-401 page works

  Scenario: Access the Hello Error-401 page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/hello/error-401"
    Then the response status code should be 401
    And I should see "Unauthorized" in the response
    And I should see "You need permission to see this page" in the response
