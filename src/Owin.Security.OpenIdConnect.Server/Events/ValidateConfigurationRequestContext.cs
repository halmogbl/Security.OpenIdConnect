/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/Security.OpenIdConnect.Server
 * for more information concerning the license and the contributors participating to this project.
 */

using Security.OpenIdConnect.Primitives;
using Microsoft.Owin;

namespace Owin.Security.OpenIdConnect.Server
{
    /// <summary>
    /// Represents the context class associated with the
    /// <see cref="OpenIdConnectServerProvider.ValidateConfigurationRequest"/> event.
    /// </summary>
    public class ValidateConfigurationRequestContext : BaseValidatingContext
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ValidateConfigurationRequestContext"/> class.
        /// </summary>
        public ValidateConfigurationRequestContext(
            IOwinContext context,
            OpenIdConnectServerOptions options,
            OpenIdConnectRequest request)
            : base(context, options, request)
        {
            Validate();
        }
    }
}
