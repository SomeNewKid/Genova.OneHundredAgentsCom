Feature: Get Hello Error502

  Verify that the Hello Error-502 page works

  Scenario: Access the Hello Error-502 page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/hello/error-502"
    Then the response status code should be 502
    And I should see "Bad gateway" in the response
    And I should see "The website tried to talk to another service" in the response
