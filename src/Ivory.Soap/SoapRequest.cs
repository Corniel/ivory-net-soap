﻿using Microsoft.AspNetCore.Http;

namespace Ivory.Soap
{
    public static class SoapRequest
    {
        /// <summary>The name of the SOAP action header (SOAPAction).</summary>
        public static readonly string ActionHeader = "SOAPAction";

        /// <summary>Returns the specified SOAP action.</summary>
        public static string GetSoapAction(this HttpRequest request)
        {
            request?.Headers.TryGetValue(ActionHeader, out var values);
            return values.Count == 1
                ? values[0] ?? string.Empty
                : null;
       }

        /// <summary>Returns true if the <see cref="HttpRequest"/> is a SOAP request.</summary>
        public static bool IsSoapRequest(this HttpRequest request)
        {
            return request != null
                && request.Headers.TryGetValue(ActionHeader, out var values)
                && values.Count == 1;
        }
    }
}