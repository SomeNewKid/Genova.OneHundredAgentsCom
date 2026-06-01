Feature: Get Hello Error403

  Verify that the Hello Error-403 page works

  Scenario: Access the Hello Error-403 page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/hello/error-403"
    Then the response status code should be 403
    And I should see "Forbidden" in the response
    And I should see "You are not allowed to see this page" in the response
