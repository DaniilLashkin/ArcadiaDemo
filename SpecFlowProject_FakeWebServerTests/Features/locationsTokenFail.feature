Feature: 03 LocationsTokenFail	
	As an unauthenticated API user
	I want to don't be able to get access to location list
	So I cann't create, read, update and delete information.

@Get
Scenario: 07 Get data for a certain location
	Given  the location id is 1
	When I am requesting certain location's data
	Then the request isn't succesfull
		
@Get
Scenario: 08 Get data for all locations
	When I am requesting data for all locations
	Then the request isn't succesfull

@Post
Scenario: 09 Create a location record
	Given the location id is 4
	And the location name is "Location004"
	When I am creating a location record
	Then the request isn't succesfull

@Put
Scenario: 10 Update a location record
	Given the location id is 4
	And the location name is "Location005"
	When I am updating a location record
	Then the request isn't succesfull

@Delete
Scenario: 11 Delete a location record
	Given the location id is 4
	When I am deleting a location record
	Then the request isn't succesfull
