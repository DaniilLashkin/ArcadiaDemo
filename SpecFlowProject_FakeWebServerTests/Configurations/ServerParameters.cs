using System;
using System.Collections.Generic;
using System.Text;

namespace SpecFlowProject_FakeWebServerTests.Configurations
{
    public static class ServerParameters
    {
        public const string serverUrl = "http://localhost:8000";
        public const string executionEmail = "techie@email.com";
        public const string executionPassword = "techie";
        public const string accessTokenAtributeName = "access_token";
        public const string loginEndpoint = "auth/login";
        public const string locationsEndpoint = "locations";
    }
}
