﻿using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CodeGardener.Salesforce.Tests.SoapClientTests
{
    [Trait("SoapClient", "When Logged In")]
    public class WhenLoggedIn
    {
        private LoggedInTestHttpHandler http;
        private SoapClient client;

        public WhenLoggedIn()
        {
            http = new LoggedInTestHttpHandler();
            client = new SoapClient(http);
            client.LoginAsync(username: "user", password: "p@ss", token: "t0k3n").Wait();
        }

        [Fact(DisplayName = "Makes an HTTP POST that contains User, Password and Token")]
        public void MakesAnHttpPostThatContainsUserPasswordAnd()
        {
            Assert.Contains("<username>user</username>", http.LastPost);
            Assert.Contains("<password>p@sst0k3n</password>", http.LastPost);
        }

        private class LoggedInTestHttpHandler: IHttpHandler
        {
            public string LastPost { get; private set; }

            public async Task<HttpResponseMessage> PostAsync(string url, HttpContent content)
            {
                LastPost = await content.ReadAsStringAsync();
                return null;
            }
        }
    }
}