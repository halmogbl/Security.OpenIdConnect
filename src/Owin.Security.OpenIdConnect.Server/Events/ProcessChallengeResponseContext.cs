﻿/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/Security.OpenIdConnect.Server
 * for more information concerning the license and the contributors participating to this project.
 */

using Security.OpenIdConnect.Primitives;
using Microsoft.Owin;
using Microsoft.Owin.Security;

namespace Owin.Security.OpenIdConnect.Server
{
    /// <summary>
    /// Represents the context class associated with the
    /// <see cref="OpenIdConnectServerProvider.ProcessChallengeResponse"/> event.
    /// </summary>
    public class ProcessChallengeResponseContext : BaseValidatingContext
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ProcessChallengeResponseContext"/> class.
        /// </summary>
        public ProcessChallengeResponseContext(
            IOwinContext context,
            OpenIdConnectServerOptions options,
            AuthenticationProperties properties,
            OpenIdConnectRequest request,
            OpenIdConnectResponse response)
            : base(context, options, request)
        {
            Validate();
            Properties = properties;
            Response = response;
        }

        /// <summary>
        /// Gets the OpenID Connect response.
        /// </summary>
        public new OpenIdConnectResponse Response { get; }

        /// <summary>
        /// Gets or sets the authentication properties.
        /// </summary>
        public AuthenticationProperties Properties { get; set; }
    }
}
