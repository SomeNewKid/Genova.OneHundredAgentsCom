Feature: Get FavIcon

  Verify that the favicon files are available

  Scenario: Access the favicon.ico file
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/favicon.ico"
    Then the response status code should be 200
    And the response should have a body

  Scenario: Access the favicon.ico file with query
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/favicon.ico?query"
    Then the response status code should be 200
    And the response should have a body

  Scenario: Access the empty.ico file
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/.ico"
    Then the response status code should be 404

  Scenario: Access the invalid.ico file
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/invalid.ico"
    Then the response status code should be 404

  Scenario: Access the favicon-16x16.png file
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/favicon-16x16.png"
    Then the response status code should be 200
    And the response should have a body

  Scenario: Access the favicon-16x16.png file with query
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/favicon-16x16.png?query"
    Then the response status code should be 200
    And the response should have a body

  Scenario: Access the favicon-32x32.png file
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/favicon-32x32.png"
    Then the response status code should be 200
    And the response should have a body

  Scenario: Access the android-chrome-192x192.png file
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/android-chrome-192x192.png"
    Then the response status code should be 200
    And the response should have a body

  Scenario: Access the android-chrome-512x512.png file
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/android-chrome-512x512.png"
    Then the response status code should be 200
    And the response should have a body

  Scenario: Access the apple-touch-icon.png file
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/apple-touch-icon.png"
    Then the response status code should be 200
    And the response should have a body

  Scenario: Access the invalid-icon.png file
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/invalid-icon.png"
    Then the response status code should be 404

  Scenario: Access the invalid-icon.png file with query
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/invalid-icon.png?query"
    Then the response status code should be 404

  Scenario: Access the empty.png file
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/.png"
    Then the response status code should be 404

  Scenario: Access the space.png file
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/ .png"
    Then the response status code should be 404

  Scenario: Access the empty.png file with query
    Given I am a visitor to "www.100agentsin100days.com"
    When I request "/.png?apple-touch-icon.png"
    Then the response status code should be 404
