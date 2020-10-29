Feature: 01 Locations	
	As an authenticated API user
	I want to be able to get full access to location list
	So I can create, read, update and delete information.

@Get
Scenario: 02 Get data for a certain location
	Given  the location id is 1
	When I am requesting certain location's data
	Then the request is succesfull
	And  location id is 1 and location name is "Location001"
		
@Get
Scenario: 03 Get data for all locations
	When I am requesting data for all locations
	Then the request is succesfull
	And the location list contains 3 rows

@Post
Scenario: 04 Create a location record
	Given the location id is 4
	And the location name is "Location004"
	When I am creating a location record
	And I am requesting certain location's data
	Then the request is succesfull
	And  location id is 4 and location name is "Location004"

@Put
Scenario: 05 Update a location record
	Given the location id is 4
	And the location name is "Location005"
	When I am updating a location record
	And I am requesting certain location's data
	Then the request is succesfull
	And  location id is 4 and location name is "Location005"

@Delete
Scenario: 06 Delete a location record
	Given the location id is 4
	When I am deleting a location record
	Then the request is succesfull
	And the reqest for location with id 4 is not successful

		
