﻿
using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace BWClient.Identity
{
    /// <summary>
    /// Handler to ensure cookie credentials are automatically sent over with each request.
    /// </summary>
    public class CookieHandler : DelegatingHandler
    {
        /// <summary>
        /// Main method to override for the handler.
        /// </summary>
        /// <param name="request">The original request.</param>
        /// <param name="cancellationToken">The token to handle cancellations.</param>
        /// <returns>The <see cref="HttpResponseMessage"/>.</returns>
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // include cookies!
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            return base.SendAsync(request, cancellationToken);
        }
    }
}
