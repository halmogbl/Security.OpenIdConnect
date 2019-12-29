﻿/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/Security.OpenIdConnect.Server
 * for more information concerning the license and the contributors participating to this project.
 */

using Security.OpenIdConnect.Primitives;

namespace Security.OpenIdConnect.Server
{
    /// <summary>
    /// Exposes the OpenID Connect request/response
    /// attached with the current HTTP context.
    /// </summary>
    public class OpenIdConnectServerFeature
    {
        /// <summary>
        /// Gets or sets the OpenID Connect request
        /// attached with the current HTTP context.
        /// </summary>
        public OpenIdConnectRequest Request { get; set; }

        /// <summary>
        /// Gets or sets the OpenID Connect response
        /// attached with the current HTTP context.
        /// </summary>
        public OpenIdConnectResponse Response { get; set; }
    }
}
