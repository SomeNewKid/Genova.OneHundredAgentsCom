Feature: Get Hello Missing

  Verify that a missing Hello page works as expected

  Scenario: Access the English Hello Context page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/hello/missing"
    Then the response status code should be 404

  Scenario: Access the Chinese (Simplified) Hello Context page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/zh/hello/missing"
    Then the response status code should be 404

  Scenario: Access the Chinese (Traditional, Hong Kong) Hello Context page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/zh-hk/hello/missing"
    Then the response status code should be 404
