﻿using Ivory.Soap;
using Ivory.Soap.Mvc;
using Ivory.TestModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Xml.Linq;

namespace Ivory.SoapApi.Controllers
{
    [SoapController(Route = "/")]
    public class ExampleSoapController : ControllerBase, ISoapController
    {
        [SoapAction("http://ivory.net/with-header")]
        public IActionResult WithHeader([FromSoapHeader]SimpleHeader header, [FromSoapBody]SimpleBody simple)
        {
            simple.Value++;
            return this.Soap(header, simple);
        }

        [SoapAction("http://ivory.net/without-header")]
        public IActionResult WithoutHeader([FromSoapBody]SimpleBody simple)
        {
            simple.Value++;
            return this.Soap(simple);
        }

        [SoapAction("http://ivory.net/xml-withHeader")]
        public IActionResult XmlWithHeader([FromSoapHeader]XElement header, [FromSoapBody]XElement body)
        {
            return this.Soap(header: header, body: body);
        }

        [SoapAction("http://ivory.net/xml")]
        public IActionResult Xml([FromSoapBody]XElement body)
        {
            return this.Soap(body);
        }

        [SoapAction("http://ivory.net/exception")]
        public IActionResult Xml([FromSoapBody]SimpleBody body)
        {
            throw new DivideByZeroException();
        }

        public SoapWriterSettings WriterSettings
        {
            get
            {
                var settings = new SoapWriterSettings();
                settings.QualifiedNames.Add("extra", "http://ivory.net/soap-extra");

                return settings;
            }
        }
    }
}
