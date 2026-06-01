Feature: Get Hello Health

  Verify that the Hello Health page works

  Scenario: Access the Hello StyleSheet
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/hello/health"
    Then the response status code should be 200
    And I should see "Healthy" in the response
