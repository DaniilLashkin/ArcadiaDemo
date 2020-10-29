using System;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using SpecFlowProject_FakeWebServerTests.Models;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using SpecFlowProject_FakeWebServerTests.Configurations;

namespace SpecFlowProject_FakeWebServerTests.Steps
{
    [Binding]
    public class LocationsSteps
    {
        public static RestClient restClient;

        private int _locationId;
        private string _locationName;
        private string _locationUri;

        private IRestResponse _response;
        private LocationData _locationData;
        private List<LocationData> _locationList;

        //public LocationsSteps(RestClient rc) => _restClient = rc;

        #region Given
        
        [Given(@"the location id is (.*)")]
        public void GivenTheLocationIdIs(int p0)
        {
            _locationId = p0;
            _locationUri = ServerParameters.locationsEndpoint + $"/{_locationId}";            
            Console.WriteLine($"_locationUri was set as '{_locationUri}'");
        } 

        [Given(@"the location name is ""(.*)""")]
        public void GivenTheLocationNameIs(string p0) =>_locationName = p0;

        #endregion Given

        #region When

        [When(@"I am requesting certain location's data")]
        public void WhenIAmRequestingCertainLocationSData()
        {
            Console.WriteLine($"Going to get data for '{_locationUri}' endpoint.");
            var request = new RestRequest(_locationUri, Method.GET, DataFormat.Json);
            _response = restClient.Execute(request);
            try
            { _locationData = Newtonsoft.Json.JsonConvert.DeserializeObject<LocationData>(_response.Content); }
            catch
            { } 
            Console.WriteLine("Recieved location data: id = " + _locationData.Id + ", name = " + _locationData.Name);
        }

        [When(@"I am requesting data for all locations")]
        public void WhenIAmRequestingDataForAllLocations()
        {
            var request = new RestRequest(ServerParameters.locationsEndpoint, Method.GET, DataFormat.Json);
            _response = restClient.Execute(request);
            Console.WriteLine($"Recieved response content: {_response.Content}");
        }

        [When(@"I am creating a location record")]
        public void WhenIAmCreatingALocationRecord()
        {
            var request = new RestRequest(ServerParameters.locationsEndpoint, Method.POST, DataFormat.Json);
            request.AddJsonBody(new { id = _locationId, name = _locationName });
            _response = restClient.Execute(request);
        }

        [When(@"I am updating a location record")]
        public void WhenIAmUpdatingALocationRecord()
        {
            var request = new RestRequest(_locationUri, Method.PUT, DataFormat.Json);
            request.AddJsonBody(new { id = _locationId, name = _locationName });
            _response = restClient.Execute(request);
        }

        [When(@"I am deleting a location record")]
        public void WhenIAmDeletingALocationRecord()
        {
            var request = new RestRequest(_locationUri, Method.DELETE);
            _response = restClient.Execute(request);
        }

        #endregion When

        #region Then

        [Then(@"the location list contains (.*) rows")]
        public void ThenTheLocationListContainsRows(int p0)
        {
            _locationList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<LocationData>>(_response.Content);
            Assert.AreEqual(p0, _locationList.Count);
        }


        [Then(@"the request is succesfull")]
        public void ThenTheRequestIsSuccesfull()
        {
            Assert.IsTrue(_response.IsSuccessful, _response.Content);
            Console.WriteLine($"Response statuscode is '{_response.StatusCode}'.");
        }

        [Then(@"location id is (.*) and location name is ""(.*)""")]
        public void ThenLocationIdIsAndLocationNameIs(int p0, string p1)
        {
            Assert.AreEqual(p0, _locationData.Id);
            Assert.AreEqual(p1, _locationData.Name);
        }

        [Then(@"the reqest for location with id (.*) is not successful")]
        public void ThenTheReqestForLocationWithIdIsNotSuccessful(int p0)
        {
            var request = new RestRequest(_locationUri, Method.GET, DataFormat.Json);
            _response = restClient.Execute(request);
            Console.WriteLine($"Get request to the Uri '{_locationUri}' results with status code '{_response.StatusCode}'.");
            Assert.AreEqual(false, _response.IsSuccessful, _response.Content);
        }

        [Then(@"the request isn't succesfull")]
        public void ThenTheRequestIsnTSuccesfull()
        {
            Assert.AreEqual(false, _response.IsSuccessful, _response.Content);
        }

        #endregion Then

    }
}
