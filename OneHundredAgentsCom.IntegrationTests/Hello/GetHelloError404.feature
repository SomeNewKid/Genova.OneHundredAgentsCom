Feature: Get Hello Error404

  Verify that the Hello Error-404 page works

  Scenario: Access the Hello Error-404 page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/hello/error-404"
    Then the response status code should be 404
    And I should see "Not found" in the response
    And I should see "We looked for the page" in the response
