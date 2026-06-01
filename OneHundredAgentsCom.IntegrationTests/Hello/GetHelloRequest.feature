Feature: Get Hello Request

  Verify that the Hello Request page works

  Scenario: Access the English Hello Culture page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/hello/request"
    Then the response status code should be 200
    And I should see "Request: GET /hello/request" in the response

  Scenario: Access the Chinese (Simplified) Hello Culture page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/zh/hello/request"
    Then the response status code should be 404

  Scenario: Access the Chinese (Traditional, Hong Kong) Hello Culture page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/zh-hk/hello/request"
    Then the response status code should be 404
