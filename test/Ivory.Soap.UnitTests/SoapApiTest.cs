﻿using Ivory.Soap.Http;
using Ivory.SoapApi;
using Ivory.SoapApi.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ivory.Soap.UnitTests
{
    public class SoapApiTest : WebApplicationFactory<Startup>
    {
        [Test]
        public async Task PostSoapAsync()
        {
            using var client = CreateClient();

            var response = await client.PostSoapAsync(
                requestUri: new Uri(@"/", UriKind.Relative),
                soapAction: "http://ivory.net/with-header",
                header: null,
                body: new SimpleBody { Value = 16 },
                cancellationToken: default);

            //Console.WriteLine(await response.Content.ReadAsStringAsync());

            var message =await SoapMessage.LoadAsync(await response.Content.ReadAsStreamAsync(), typeof(XElement), typeof(SimpleBody));

            var actual = (SimpleBody)message.Body;

            Assert.AreEqual(17, actual.Value);
        }
    }
}
