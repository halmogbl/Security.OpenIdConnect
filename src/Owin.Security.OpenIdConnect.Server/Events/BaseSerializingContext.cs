﻿/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/Security.OpenIdConnect.Server
 * for more information concerning the license and the contributors participating to this project.
 */

using Security.OpenIdConnect.Primitives;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Provider;

namespace Owin.Security.OpenIdConnect.Server
{
    /// <summary>
    /// Represents an abstract base class used for certain event contexts.
    /// </summary>
    public abstract class BaseSerializingContext : BaseContext<OpenIdConnectServerOptions>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="DeserializeAccessTokenContext"/> class.
        /// </summary>
        public BaseSerializingContext(
            IOwinContext context,
            OpenIdConnectServerOptions options,
            OpenIdConnectRequest request,
            OpenIdConnectResponse response,
            AuthenticationTicket ticket)
            : base(context, options)
        {
            Request = request;
            Response = response;
            Ticket = ticket;
        }

        /// <summary>
        /// Gets the OpenID Connect request.
        /// </summary>
        public new OpenIdConnectRequest Request { get; }

        /// <summary>
        /// Gets the OpenID Connect response.
        /// </summary>
        public new OpenIdConnectResponse Response { get; }

        /// <summary>
        /// Gets the authentication ticket.
        /// </summary>
        public AuthenticationTicket Ticket { get; }

        /// <summary>
        /// Gets a boolean indicating whether the
        /// <see cref="HandleSerialization()"/> method was called.
        /// </summary>
        public bool IsHandled { get; private set; }

        /// <summary>
        /// Marks the serialization process as handled by the application code.
        /// </summary>
        public virtual void HandleSerialization() => IsHandled = true;
    }
}
