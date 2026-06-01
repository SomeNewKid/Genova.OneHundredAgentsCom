Feature: Get Hello Error400

  Verify that the Hello Error-400 page works

  Scenario: Access the Hello Error-400 page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/hello/error-400"
    Then the response status code should be 400
    And I should see "Bad request" in the response
    And I should see "The request arrived in the wrong shape" in the response
