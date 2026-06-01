Feature: Get Sitemap

  Verify that the sitemap page includes the agent catalogue

  Scenario: Access the sitemap page
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/sitemap"
    Then the response status code should be 200
    And I should see "<h1>Sitemap</h1>" in the response
    And I should see "<h2>IBM BeeAI Framework</h2>" in the response
    And I should see "/travel-landmark-agent" as a hyperlink location
    And I should see "Travel Landmark Agent" in the response
