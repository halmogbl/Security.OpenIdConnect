/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/Security.OpenIdConnect.Server
 * for more information concerning the license and the contributors participating to this project.
 */

using Security.OpenIdConnect.Primitives;
using Microsoft.Owin;
using Microsoft.Owin.Security.Notifications;

namespace Owin.Security.OpenIdConnect.Server
{
    /// <summary>
    /// Represents the context class associated with the
    /// <see cref="OpenIdConnectServerProvider.ApplyConfigurationResponse"/> event.
    /// </summary>
    public class ApplyConfigurationResponseContext : BaseNotification<OpenIdConnectServerOptions>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ApplyConfigurationResponseContext"/> class.
        /// </summary>
        public ApplyConfigurationResponseContext(
            IOwinContext context,
            OpenIdConnectServerOptions options,
            OpenIdConnectRequest request,
            OpenIdConnectResponse response)
            : base(context, options)
        {
            Request = request;
            Response = response;
        }

        /// <summary>
        /// Gets the configuration request.
        /// </summary>
        /// <remarks>
        /// Note: this property may be null if an error occurred while
        /// extracting the configuration request from the HTTP request.
        /// </remarks>
        public new OpenIdConnectRequest Request { get; }

        /// <summary>
        /// Gets the configuration response.
        /// </summary>
        public new OpenIdConnectResponse Response { get; }

        /// <summary>
        /// Gets the error code returned to the client application.
        /// When the response indicates a successful response,
        /// this property returns <c>null</c>.
        /// </summary>
        public string Error => Response.Error;
    }
}
