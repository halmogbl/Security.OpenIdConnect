# Security.OpenIdConnect
AspNet.Security.OpenIdConnect.Server is an advanced OAuth2/OpenID Connect server framework for both ASP.NET Core 1.x/2.x and OWIN/Katana 3.x/4.x, designed to offer a low-level, protocol-first approach.


Get started

Based on OAuthAuthorizationServerMiddleware from Katana, AspNet.Security.OpenIdConnect.Server exposes similar primitives and can be directly registered in Startup.cs using the UseOpenIdConnectServer extension method:

