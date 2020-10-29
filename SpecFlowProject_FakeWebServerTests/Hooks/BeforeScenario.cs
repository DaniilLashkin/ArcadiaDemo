using System;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using SpecFlowProject_FakeWebServerTests.Models;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using SpecFlowProject_FakeWebServerTests.Configurations;
using SpecFlowProject_FakeWebServerTests.Steps;

namespace SpecFlowProject_FakeWebServerTests.Hooks
{
    [Binding]
    public class BeforeScenario
    {
        //private RestClient _rc;
        //public BeforeScenario(RestClient rc) => _rc = rc;

        [BeforeScenario(Order = 0)]
        [Scope(Feature = "01 Locations")]
        public void PrepareRestClientWithJwt()
        {
            Console.WriteLine($"Going to login to '{ServerParameters.serverUrl}' with email '{ServerParameters.executionEmail}' and password '{ServerParameters.executionPassword}'.");

            var requestJsonBody = new { email = ServerParameters.executionEmail, password = ServerParameters.executionPassword };
            var request = new RestRequest(ServerParameters.loginEndpoint, Method.POST, DataFormat.Json);
            request.AddJsonBody(requestJsonBody);
            var response = new RestClient(ServerParameters.serverUrl).Execute(request);
            string jwt = JToken.Parse(response.Content).SelectToken(ServerParameters.accessTokenAtributeName).ToObject<string>();
            var rc = new RestClient(ServerParameters.serverUrl);
            rc.Authenticator = new JwtAuthenticator(jwt);

            LocationsSteps.restClient = rc;
        }

        [BeforeScenario(Order = 0)]
        [Scope(Feature = "03 LocationsTokenFail")]
        public void PrepareRestClientWithoutJwt()
        {            
            var rc = new RestClient(ServerParameters.serverUrl);
            LocationsSteps.restClient = rc;
        }
    }
}
