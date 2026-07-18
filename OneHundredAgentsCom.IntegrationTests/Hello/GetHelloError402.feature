Feature: Get Hello Error402

  Verify that the Hello Error-402 page works 

  Scenario: Access the Hello Error-402 page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/hello/error-402"
    Then the response status code should be 402
    And I should see "Something went wrong" in the response
    And I should see "The website hit an error" in the response
