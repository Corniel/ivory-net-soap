#pragma warning disable S1199 // Nested code blocks should not be used
// Nested code is used here to make the XML Writing more readable.

using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace Ivory.Soap.Mvc
{
    /// <summary>Represents a SOAP envelope <see cref="IActionResult"/>.</summary>
    public class SoapResult : IActionResult
    {
        /// <summary>Initializes a new instance of the <see cref="SoapResult"/> class.</summary>
        /// <param name="header">
        /// The SOAP header.
        /// </param>
        /// <param name="body">
        /// The SOAP body.
        /// </param>
        public SoapResult(object header, object body)
        {
            Header = header;
            Body = body;
        }

        /// <summary>Gets the SOAP header.</summary>
        public object Header { get; }

        /// <summary>Gets the SOAP body.</summary>
        public object Body { get; }

        /// <summary>Writes the SOAP message asynchronously to the response.</summary>
        /// <param name="context">
        /// The context in which the result is executed. The context information
        /// includes information about the action that was executed and request
        /// information.
        /// </param>
        public Task ExecuteResultAsync(ActionContext context)
        {
            Guard.NotNull(context, nameof(context));

            var buffer = new MemoryStream();
            var writer = XmlWriter.Create(buffer, WriterSettings);
            var message = new SoapMessage(Header, Body);
            message.Save(writer, WriterSettings);

            buffer.Position = 0;

            return buffer.CopyToAsync(context.HttpContext.Response.Body);
        }

        /// <summary>Gets the <see cref="SoapWriterSettings"/> to use.</summary>
        protected virtual SoapWriterSettings WriterSettings { get; } = new SoapWriterSettings();
    }
}
