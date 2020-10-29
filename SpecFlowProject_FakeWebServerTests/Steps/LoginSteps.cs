using System;
using TechTalk.SpecFlow;
using SpecFlowProject_FakeWebServerTests.Configurations;
using RestSharp;
using RestSharp.Authenticators;
using NUnit.Framework;
using Newtonsoft.Json.Linq;

namespace SpecFlowProject_FakeWebServerTests.Steps
{
    [Binding]
    public class LoginSteps
    {
        private string _jwt = string.Empty;
        private object _requestJsonBody = string.Empty;
        private IRestResponse _response;


        [Given(@"the username is ""(.*)"" and the password is ""(.*)""")]
        public void GivenTheUsernameIsAndThePasswordIs(string p0, string p1)
        {
            _requestJsonBody = new { email = p0, password = p1 };
        }

        [When(@"I request login to the server")]
        public void WhenIRequestLoginToTheServer()
        {
            var request = new RestRequest(ServerParameters.loginEndpoint, Method.POST, DataFormat.Json);
            request.AddJsonBody(_requestJsonBody);                
            _response = new RestClient(ServerParameters.serverUrl).Execute(request);
        }

        [Then(@"the login request is succesfull")]
        public void ThenTheLoginRequestIsSuccesfull()
        {
            Assert.IsTrue(_response.IsSuccessful, $"The login request wasn't successful.");
            Console.WriteLine($"Response status code is '{_response.StatusCode}'.");
        }

        [Then(@"the recieved token is valid")]
        public void ThenTheRecievedTokenIsValid()
        {
            string jwt = JToken.Parse(_response.Content).SelectToken(ServerParameters.accessTokenAtributeName).ToObject<string>();
            var rc = new RestClient(ServerParameters.serverUrl);
            rc.Authenticator = new JwtAuthenticator(jwt);

            var request = new RestRequest(ServerParameters.locationsEndpoint, Method.GET, DataFormat.Json);
            _response = rc.Execute(request);
            Console.WriteLine($"Get request to the Uri '{ServerParameters.locationsEndpoint}' results with status code '{_response.StatusCode}'.");
            Assert.AreEqual(true, _response.IsSuccessful, _response.Content);
        }
    }
}
