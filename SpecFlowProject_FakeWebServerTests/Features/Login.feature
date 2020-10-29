Feature: 00 Login
	In order to operates with fake API server
	As a registered user with email and password
	I want to be able to log in and get jwToken

@Authentification
Scenario: 01 login with username and password
	Given the username is "techie@email.com" and the password is "techie"
	When I request login to the server
	Then the login request is succesfull
	And the recieved token is valid