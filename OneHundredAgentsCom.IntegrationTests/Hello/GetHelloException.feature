Feature: Get Hello Exception

  Verify that the Hello Exception page works

  Scenario: Access the English Hello Exception page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/hello/exception"
    Then the response status code should be 500
    And I should see "Internal server error" in the response
    And I should see "The website tripped over something internal" in the response

  Scenario: Access the Chinese (Simplified) Hello Exception page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/zh/hello/exception"
    Then the response status code should be 404

  Scenario: Access the Chinese (Traditional, Hong Kong) Hello Exception page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/zh-hk/hello/exception"
    Then the response status code should be 404
