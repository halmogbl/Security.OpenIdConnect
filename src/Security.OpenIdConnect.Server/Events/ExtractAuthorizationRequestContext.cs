/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/Security.OpenIdConnect.Server
 * for more information concerning the license and the contributors participating to this project.
 */

using Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.OpenIdConnect.Server
{
    /// <summary>
    /// Represents the context class associated with the
    /// <see cref="OpenIdConnectServerProvider.ExtractAuthorizationRequest"/> event.
    /// </summary>
    public class ExtractAuthorizationRequestContext : BaseValidatingContext
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ExtractAuthorizationRequestContext"/> class.
        /// </summary>
        public ExtractAuthorizationRequestContext(
            HttpContext context,
            AuthenticationScheme scheme,
            OpenIdConnectServerOptions options,
            OpenIdConnectRequest request)
            : base(context, scheme, options, request)
        {
            Validate();
        }
    }
}