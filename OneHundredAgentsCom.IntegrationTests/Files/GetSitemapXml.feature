Feature: Get Sitemap XML

  Verify that the sitemap.xml file is available

  Scenario: Access the sitemap.xml file
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/sitemap.xml"
    Then the response status code should be 200
    And I should see "urlset" in the response
    And I should see "<url>" in the response
    And I should see "<loc>" in the response

    And I should see "/" as a sitemap location
    And I should see "/sitemap" as a sitemap location
    And I should see "/travel-landmark-agent" as a sitemap location

    And I should see "https://www.100agentsin100days.com" in the response
    And I should not see "https://www.example.com" in the response
    And I should not see "https://www.example.net" in the response
    And I should not see "https://www.nibblon.com" in the response
    And I should not see "https://www.nibblon.net" in the response
    And I should not see "https://www.grubenwald" in the response
    And I should not see "https://www.grubenwald" in the response


  Scenario: Access the sitemap.xml file with query
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/sitemap.xml?query"
    Then the response status code should be 200
    And I should see "urlset" in the response
    And I should see "<url>" in the response
    And I should see "<loc>" in the response
    And I should see "https://www.100agentsin100days.com" in the response

  Scenario: Access the missing.xml file
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/missing.xml"
    Then the response status code should be 404
