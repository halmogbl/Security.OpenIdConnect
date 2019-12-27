﻿/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/AspNet.Security.OpenIdConnect.Server
 * for more information concerning the license and the contributors participating to this project.
 */

using System;
using Xunit;

namespace AspNet.Security.OpenIdConnect.Primitives.Tests
{
    public class OpenIdConnectExtensionsTests
    {
        [Fact]
        public void GetAcrValues_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.GetAcrValues();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, new string[0])]
        [InlineData("mod-pr", new[] { "mod-pr" })]
        [InlineData("mod-pr ", new[] { "mod-pr" })]
        [InlineData(" mod-pr ", new[] { "mod-pr" })]
        [InlineData("mod-pr mod-mf", new[] { "mod-pr", "mod-mf" })]
        [InlineData("mod-pr     mod-mf", new[] { "mod-pr", "mod-mf" })]
        [InlineData("mod-pr mod-mf ", new[] { "mod-pr", "mod-mf" })]
        [InlineData(" mod-pr mod-mf", new[] { "mod-pr", "mod-mf" })]
        [InlineData("mod-pr mod-pr mod-mf", new[] { "mod-pr", "mod-mf" })]
        [InlineData("mod-pr MOD-PR mod-mf", new[] { "mod-pr", "MOD-PR", "mod-mf" })]
        public void GetAcrValues_ReturnsExpectedAcrValues(string value, string[] values)
        {
            // Arrange
            var request = new OpenIdConnectRequest
            {
                AcrValues = value
            };

            // Act and assert
            Assert.Equal(values, request.GetAcrValues());
        }

        [Fact]
        public void GetScopes_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.GetScopes();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, new string[0])]
        [InlineData("openid", new[] { "openid" })]
        [InlineData("openid ", new[] { "openid" })]
        [InlineData(" openid ", new[] { "openid" })]
        [InlineData("openid profile", new[] { "openid", "profile" })]
        [InlineData("openid     profile", new[] { "openid", "profile" })]
        [InlineData("openid profile ", new[] { "openid", "profile" })]
        [InlineData(" openid profile", new[] { "openid", "profile" })]
        [InlineData("openid openid profile", new[] { "openid", "profile" })]
        [InlineData("openid OPENID profile", new[] { "openid", "OPENID", "profile" })]
        public void GetScopes_ReturnsExpectedScopes(string scope, string[] scopes)
        {
            // Arrange
            var request = new OpenIdConnectRequest
            {
                Scope = scope
            };

            // Act and assert
            Assert.Equal(scopes, request.GetScopes());
        }

        [Fact]
        public void HasAcrValue_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.HasAcrValue("mod-mf");
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void HasAcrValue_ThrowsAnExceptionForNullOrEmptyAcrValue(string value)
        {
            // Arrange
            var request = new OpenIdConnectRequest();

            // Act and assert
            var exception = Assert.Throws<ArgumentException>(delegate
            {
                request.HasAcrValue(value);
            });

            Assert.Equal("value", exception.ParamName);
            Assert.StartsWith("The value cannot be null or empty.", exception.Message);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("mod-mf", true)]
        [InlineData("mod-mf mod-pr", true)]
        [InlineData(" mod-mf mod-pr", true)]
        [InlineData("mod-pr mod-mf", true)]
        [InlineData("mod-pr mod-mf ", true)]
        [InlineData("mod-pr mod-mf mod-cstm", true)]
        [InlineData("mod-pr mod-mf mod-cstm ", true)]
        [InlineData("mod-pr    mod-mf   mod-cstm ", true)]
        [InlineData("mod-pr", false)]
        [InlineData("mod-pr mod-cstm", false)]
        [InlineData("MOD-MF", false)]
        [InlineData("MOD-MF MOD-PR", false)]
        [InlineData(" MOD-MF MOD-PR", false)]
        [InlineData("MOD-PR MOD-MF", false)]
        [InlineData("MOD-PR MOD-MF ", false)]
        [InlineData("MOD-PR MOD-MF MOD-CSTM", false)]
        [InlineData("MOD-PR MOD-MF MOD-CSTM ", false)]
        [InlineData("MOD-PR    MOD-MF   MOD-CSTM ", false)]
        [InlineData("MOD-PR", false)]
        [InlineData("MOD-PR MOD-CSTM", false)]
        public void HasAcrValue_ReturnsExpectedResult(string value, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest
            {
                AcrValues = value
            };

            // Act and assert
            Assert.Equal(result, request.HasAcrValue("mod-mf"));
        }

        [Fact]
        public void HasPrompt_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.HasPrompt(OpenIdConnectConstants.Prompts.Consent);
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void HasPrompt_ThrowsAnExceptionForNullOrEmptyPrompt(string prompt)
        {
            // Arrange
            var request = new OpenIdConnectRequest();

            // Act and assert
            var exception = Assert.Throws<ArgumentException>(delegate
            {
                request.HasPrompt(prompt);
            });

            Assert.Equal("prompt", exception.ParamName);
            Assert.StartsWith("The prompt cannot be null or empty.", exception.Message);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("consent", true)]
        [InlineData("consent login", true)]
        [InlineData(" consent login", true)]
        [InlineData("login consent", true)]
        [InlineData("login consent ", true)]
        [InlineData("login consent select_account", true)]
        [InlineData("login consent select_account ", true)]
        [InlineData("login    consent   select_account ", true)]
        [InlineData("login", false)]
        [InlineData("login select_account", false)]
        [InlineData("CONSENT", false)]
        [InlineData("CONSENT LOGIN", false)]
        [InlineData(" CONSENT LOGIN", false)]
        [InlineData("LOGIN CONSENT", false)]
        [InlineData("LOGIN CONSENT ", false)]
        [InlineData("LOGIN CONSENT SELECT_ACCOUNT", false)]
        [InlineData("LOGIN CONSENT SELECT_ACCOUNT ", false)]
        [InlineData("LOGIN    CONSENT   SELECT_ACCOUNT ", false)]
        [InlineData("LOGIN", false)]
        [InlineData("LOGIN SELECT_ACCOUNT", false)]
        public void HasPrompt_ReturnsExpectedResult(string prompt, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest
            {
                Prompt = prompt
            };

            // Act and assert
            Assert.Equal(result, request.HasPrompt(OpenIdConnectConstants.Prompts.Consent));
        }

        [Fact]
        public void HasResponseType_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.HasResponseType(OpenIdConnectConstants.ResponseTypes.Code);
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void HasResponseType_ThrowsAnExceptionForNullOrEmptyResponseType(string type)
        {
            // Arrange
            var request = new OpenIdConnectRequest();

            // Act and assert
            var exception = Assert.Throws<ArgumentException>(delegate
            {
                request.HasResponseType(type);
            });

            Assert.Equal("type", exception.ParamName);
            Assert.StartsWith("The response type cannot be null or empty.", exception.Message);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("code", true)]
        [InlineData("code id_token", true)]
        [InlineData(" code id_token", true)]
        [InlineData("id_token code", true)]
        [InlineData("id_token code ", true)]
        [InlineData("id_token code token", true)]
        [InlineData("id_token code token ", true)]
        [InlineData("id_token    code   token ", true)]
        [InlineData("id_token", false)]
        [InlineData("id_token token", false)]
        [InlineData("CODE", false)]
        [InlineData("CODE ID_TOKEN", false)]
        [InlineData(" CODE ID_TOKEN", false)]
        [InlineData("ID_TOKEN CODE", false)]
        [InlineData("ID_TOKEN CODE ", false)]
        [InlineData("ID_TOKEN CODE TOKEN", false)]
        [InlineData("ID_TOKEN CODE TOKEN ", false)]
        [InlineData("ID_TOKEN    CODE   TOKEN ", false)]
        [InlineData("ID_TOKEN", false)]
        [InlineData("ID_TOKEN TOKEN", false)]
        public void HasResponseType_ReturnsExpectedResult(string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest
            {
                ResponseType = type
            };

            // Act and assert
            Assert.Equal(result, request.HasResponseType(OpenIdConnectConstants.ResponseTypes.Code));
        }

        [Fact]
        public void HasScope_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.HasScope(OpenIdConnectConstants.Scopes.OpenId);
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void HasScope_ThrowsAnExceptionForNullOrEmptyScope(string scope)
        {
            // Arrange
            var request = new OpenIdConnectRequest();

            // Act and assert
            var exception = Assert.Throws<ArgumentException>(delegate
            {
                request.HasScope(scope);
            });

            Assert.Equal("scope", exception.ParamName);
            Assert.StartsWith("The scope cannot be null or empty.", exception.Message);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("openid", true)]
        [InlineData("openid ", true)]
        [InlineData(" openid ", true)]
        [InlineData("openid profile", true)]
        [InlineData("openid     profile", true)]
        [InlineData("openid profile ", true)]
        [InlineData(" openid profile", true)]
        [InlineData("profile", false)]
        [InlineData("profile email", false)]
        [InlineData("OPENID", false)]
        [InlineData("OPENID ", false)]
        [InlineData(" OPENID ", false)]
        [InlineData("OPENID PROFILE", false)]
        [InlineData("OPENID     PROFILE", false)]
        [InlineData("OPENID PROFILE ", false)]
        [InlineData(" OPENID PROFILE", false)]
        [InlineData("PROFILE", false)]
        [InlineData("PROFILE EMAIL", false)]
        public void HasScope_ReturnsExpectedResult(string scope, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest
            {
                Scope = scope
            };

            // Act and assert
            Assert.Equal(result, request.HasScope(OpenIdConnectConstants.Scopes.OpenId));
        }

        [Fact]
        public void IsAuthorizationRequest_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsAuthorizationRequest();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData(OpenIdConnectConstants.MessageTypes.AuthorizationRequest, true)]
        public void IsAuthorizationRequest_ReturnsExpectedResult(string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest();
            request.SetProperty(OpenIdConnectConstants.Properties.MessageType, type);

            // Act and assert
            Assert.Equal(result, request.IsAuthorizationRequest());
        }

        [Fact]
        public void IsConfidential_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsConfidential();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData(OpenIdConnectConstants.ConfidentialityLevels.Private, true)]
        [InlineData(OpenIdConnectConstants.ConfidentialityLevels.Public, false)]
        public void IsConfidential_ReturnsExpectedResult(string level, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest();
            request.SetProperty(OpenIdConnectConstants.Properties.ConfidentialityLevel, level);

            // Act and assert
            Assert.Equal(result, request.IsConfidential());
        }

        [Fact]
        public void IsConfigurationRequest_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsConfigurationRequest();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData(OpenIdConnectConstants.MessageTypes.ConfigurationRequest, true)]
        public void IsConfigurationRequest_ReturnsExpectedResult(string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest();
            request.SetProperty(OpenIdConnectConstants.Properties.MessageType, type);

            // Act and assert
            Assert.Equal(result, request.IsConfigurationRequest());
        }

        [Fact]
        public void IsCryptographyRequest_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsCryptographyRequest();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData(OpenIdConnectConstants.MessageTypes.CryptographyRequest, true)]
        public void IsCryptographyRequest_ReturnsExpectedResult(string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest();
            request.SetProperty(OpenIdConnectConstants.Properties.MessageType, type);

            // Act and assert
            Assert.Equal(result, request.IsCryptographyRequest());
        }

        [Fact]
        public void IsIntrospectionRequest_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsIntrospectionRequest();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData(OpenIdConnectConstants.MessageTypes.IntrospectionRequest, true)]
        public void IsIntrospectionRequest_ReturnsExpectedResult(string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest();
            request.SetProperty(OpenIdConnectConstants.Properties.MessageType, type);

            // Act and assert
            Assert.Equal(result, request.IsIntrospectionRequest());
        }

        [Fact]
        public void IsLogoutRequest_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsLogoutRequest();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData(OpenIdConnectConstants.MessageTypes.LogoutRequest, true)]
        public void IsLogoutRequest_ReturnsExpectedResult(string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest();
            request.SetProperty(OpenIdConnectConstants.Properties.MessageType, type);

            // Act and assert
            Assert.Equal(result, request.IsLogoutRequest());
        }

        [Fact]
        public void IsRevocationRequest_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsRevocationRequest();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData(OpenIdConnectConstants.MessageTypes.RevocationRequest, true)]
        public void IsRevocationRequest_ReturnsExpectedResult(string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest();
            request.SetProperty(OpenIdConnectConstants.Properties.MessageType, type);

            // Act and assert
            Assert.Equal(result, request.IsRevocationRequest());
        }

        [Fact]
        public void IsTokenRequest_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsTokenRequest();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData(OpenIdConnectConstants.MessageTypes.TokenRequest, true)]
        public void IsTokenRequest_ReturnsExpectedResult(string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest();
            request.SetProperty(OpenIdConnectConstants.Properties.MessageType, type);

            // Act and assert
            Assert.Equal(result, request.IsTokenRequest());
        }

        [Fact]
        public void IsUserinfoRequest_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsUserinfoRequest();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData(OpenIdConnectConstants.MessageTypes.UserinfoRequest, true)]
        public void IsUserinfoRequest_ReturnsExpectedResult(string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest();
            request.SetProperty(OpenIdConnectConstants.Properties.MessageType, type);

            // Act and assert
            Assert.Equal(result, request.IsUserinfoRequest());
        }

        [Fact]
        public void IsNoneFlow_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsNoneFlow();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData("none", true)]
        [InlineData("none ", true)]
        [InlineData(" none", true)]
        [InlineData("none id_token", false)]
        [InlineData(" none id_token", false)]
        [InlineData("none id_token ", false)]
        [InlineData(" none id_token ", false)]
        [InlineData("NONE", false)]
        [InlineData("NONE ", false)]
        [InlineData(" NONE", false)]
        [InlineData("NONE ID_TOKEN", false)]
        [InlineData(" NONE ID_TOKEN", false)]
        [InlineData("NONE ID_TOKEN ", false)]
        [InlineData(" NONE ID_TOKEN ", false)]
        public void IsNoneFlow_ReturnsExpectedResult(string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest
            {
                ResponseType = type
            };

            // Act and assert
            Assert.Equal(result, request.IsNoneFlow());
        }

        [Fact]
        public void IsAuthorizationCodeFlow_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsAuthorizationCodeFlow();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData("code", true)]
        [InlineData("code ", true)]
        [InlineData(" code", true)]
        [InlineData("code id_token", false)]
        [InlineData(" code id_token", false)]
        [InlineData("code id_token ", false)]
        [InlineData(" code id_token ", false)]
        [InlineData("CODE", false)]
        [InlineData("CODE ", false)]
        [InlineData(" CODE", false)]
        [InlineData("CODE ID_TOKEN", false)]
        [InlineData(" CODE ID_TOKEN", false)]
        [InlineData("CODE ID_TOKEN ", false)]
        [InlineData(" CODE ID_TOKEN ", false)]
        public void IsAuthorizationCodeFlow_ReturnsExpectedResult(string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest
            {
                ResponseType = type
            };

            // Act and assert
            Assert.Equal(result, request.IsAuthorizationCodeFlow());
        }

        [Fact]
        public void IsImplicitFlow_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsImplicitFlow();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData("id_token", true)]
        [InlineData("id_token ", true)]
        [InlineData(" id_token", true)]
        [InlineData("id_token token", true)]
        [InlineData(" id_token token", true)]
        [InlineData("id_token token ", true)]
        [InlineData(" id_token token ", true)]
        [InlineData("token", true)]
        [InlineData("token ", true)]
        [InlineData(" token", true)]
        [InlineData("code id_token", false)]
        [InlineData("code id_token token", false)]
        [InlineData("code token", false)]
        [InlineData("ID_TOKEN", false)]
        [InlineData("ID_TOKEN ", false)]
        [InlineData(" ID_TOKEN", false)]
        [InlineData("ID_TOKEN TOKEN", false)]
        [InlineData(" ID_TOKEN TOKEN", false)]
        [InlineData("ID_TOKEN TOKEN ", false)]
        [InlineData(" ID_TOKEN TOKEN ", false)]
        [InlineData("TOKEN", false)]
        [InlineData("TOKEN ", false)]
        [InlineData(" TOKEN", false)]
        [InlineData("CODE ID_TOKEN", false)]
        [InlineData("CODE ID_TOKEN TOKEN", false)]
        [InlineData("CODE TOKEN", false)]
        public void IsImplicitFlow_ReturnsExpectedResult(string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest
            {
                ResponseType = type
            };

            // Act and assert
            Assert.Equal(result, request.IsImplicitFlow());
        }

        [Fact]
        public void IsHybridFlow_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsHybridFlow();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData("code id_token", true)]
        [InlineData("code id_token ", true)]
        [InlineData(" code id_token", true)]
        [InlineData("code id_token token", true)]
        [InlineData(" code id_token token", true)]
        [InlineData("code id_token token ", true)]
        [InlineData(" code id_token token ", true)]
        [InlineData(" code  id_token  token ", true)]
        [InlineData("code token", true)]
        [InlineData("code token ", true)]
        [InlineData(" code token", true)]
        [InlineData("id_token", false)]
        [InlineData("id_token token", false)]
        [InlineData("token", false)]
        [InlineData("CODE ID_TOKEN", false)]
        [InlineData("CODE ID_TOKEN ", false)]
        [InlineData(" CODE ID_TOKEN", false)]
        [InlineData("CODE ID_TOKEN TOKEN", false)]
        [InlineData(" CODE ID_TOKEN TOKEN", false)]
        [InlineData("CODE ID_TOKEN TOKEN ", false)]
        [InlineData(" CODE ID_TOKEN TOKEN ", false)]
        [InlineData(" CODE  ID_TOKEN  TOKEN ", false)]
        [InlineData("CODE TOKEN", false)]
        [InlineData("CODE TOKEN ", false)]
        [InlineData(" CODE TOKEN", false)]
        [InlineData("ID_TOKEN", false)]
        [InlineData("ID_TOKEN TOKEN", false)]
        [InlineData("TOKEN", false)]
        public void IsHybridFlow_ReturnsExpectedResult(string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest
            {
                ResponseType = type
            };

            // Act and assert
            Assert.Equal(result, request.IsHybridFlow());
        }

        [Fact]
        public void IsFragmentResponseMode_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsFragmentResponseMode();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, null, false)]
        [InlineData("unknown", null, false)]
        [InlineData("query", null, false)]
        [InlineData("form_post", null, false)]
        [InlineData("fragment", null, true)]
        [InlineData("fragment ", null, false)]
        [InlineData(" fragment", null, false)]
        [InlineData(" fragment ", null, false)]
        [InlineData(null, "code", false)]
        [InlineData(null, "code id_token", true)]
        [InlineData(null, "code id_token token", true)]
        [InlineData(null, "code token", true)]
        [InlineData(null, "id_token", true)]
        [InlineData(null, "id_token token", true)]
        [InlineData(null, "token", true)]
        [InlineData("QUERY", null, false)]
        [InlineData("FRAGMENT", null, false)]
        [InlineData("FORM_POST", null, false)]
        [InlineData(null, "CODE", false)]
        [InlineData(null, "CODE ID_TOKEN", false)]
        [InlineData(null, "CODE ID_TOKEN TOKEN", false)]
        [InlineData(null, "CODE TOKEN", false)]
        [InlineData(null, "ID_TOKEN", false)]
        [InlineData(null, "ID_TOKEN TOKEN", false)]
        [InlineData(null, "TOKEN", false)]
        public void IsFragmentResponseMode_ReturnsExpectedResult(string mode, string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest
            {
                ResponseMode = mode,
                ResponseType = type
            };

            // Act and assert
            Assert.Equal(result, request.IsFragmentResponseMode());
        }

        [Fact]
        public void IsQueryResponseMode_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsQueryResponseMode();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, null, false)]
        [InlineData("unknown", null, false)]
        [InlineData("query", null, true)]
        [InlineData("query ", null, false)]
        [InlineData(" query", null, false)]
        [InlineData(" query ", null, false)]
        [InlineData("fragment", null, false)]
        [InlineData("form_post", null, false)]
        [InlineData(null, "none", true)]
        [InlineData(null, "code", true)]
        [InlineData(null, "code id_token token", false)]
        [InlineData(null, "code token", false)]
        [InlineData(null, "id_token", false)]
        [InlineData(null, "id_token token", false)]
        [InlineData(null, "token", false)]
        [InlineData("QUERY", null, false)]
        [InlineData("FRAGMENT", null, false)]
        [InlineData("FORM_POST", null, false)]
        [InlineData(null, "CODE", false)]
        [InlineData(null, "CODE ID_TOKEN", false)]
        [InlineData(null, "CODE ID_TOKEN TOKEN", false)]
        [InlineData(null, "CODE TOKEN", false)]
        [InlineData(null, "ID_TOKEN", false)]
        [InlineData(null, "ID_TOKEN TOKEN", false)]
        [InlineData(null, "TOKEN", false)]
        public void IsQueryResponseMode_ReturnsExpectedResult(string mode, string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest
            {
                ResponseMode = mode,
                ResponseType = type
            };

            // Act and assert
            Assert.Equal(result, request.IsQueryResponseMode());
        }

        [Fact]
        public void IsFormPostResponseMode_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsFormPostResponseMode();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData("query", false)]
        [InlineData("fragment", false)]
        [InlineData("form_post", true)]
        [InlineData("form_post ", false)]
        [InlineData(" form_post", false)]
        [InlineData(" form_post ", false)]
        [InlineData("QUERY", false)]
        [InlineData("FRAGMENT", false)]
        [InlineData("FORM_POST", false)]
        public void IsFormPostResponseMode_ReturnsExpectedResult(string mode, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest
            {
                ResponseMode = mode
            };

            // Act and assert
            Assert.Equal(result, request.IsFormPostResponseMode());
        }

        [Fact]
        public void IsAuthorizationCodeGrantType_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsAuthorizationCodeGrantType();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData("authorization_code", true)]
        [InlineData("authorization_code ", false)]
        [InlineData(" authorization_code", false)]
        [InlineData(" authorization_code ", false)]
        [InlineData("client_credentials", false)]
        [InlineData("password", false)]
        [InlineData("refresh_token", false)]
        [InlineData("AUTHORIZATION_CODE", false)]
        [InlineData("CLIENT_CREDENTIALS", false)]
        [InlineData("PASSWORD", false)]
        [InlineData("REFRESH_TOKEN", false)]
        public void IsAuthorizationCodeGrantType_ReturnsExpectedResult(string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest
            {
                GrantType = type
            };

            // Act and assert
            Assert.Equal(result, request.IsAuthorizationCodeGrantType());
        }

        [Fact]
        public void IsClientCredentialsGrantType_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsClientCredentialsGrantType();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData("authorization_code", false)]
        [InlineData("client_credentials", true)]
        [InlineData("client_credentials ", false)]
        [InlineData(" client_credentials", false)]
        [InlineData(" client_credentials ", false)]
        [InlineData("password", false)]
        [InlineData("refresh_token", false)]
        [InlineData("AUTHORIZATION_CODE", false)]
        [InlineData("CLIENT_CREDENTIALS", false)]
        [InlineData("PASSWORD", false)]
        [InlineData("REFRESH_TOKEN", false)]
        public void IsClientCredentialsGrantType_ReturnsExpectedResult(string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest
            {
                GrantType = type
            };

            // Act and assert
            Assert.Equal(result, request.IsClientCredentialsGrantType());
        }

        [Fact]
        public void IsPasswordGrantType_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsPasswordGrantType();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData("authorization_code", false)]
        [InlineData("client_credentials", false)]
        [InlineData("password", true)]
        [InlineData("password ", false)]
        [InlineData(" password", false)]
        [InlineData(" password ", false)]
        [InlineData("refresh_token", false)]
        [InlineData("AUTHORIZATION_CODE", false)]
        [InlineData("CLIENT_CREDENTIALS", false)]
        [InlineData("PASSWORD", false)]
        [InlineData("REFRESH_TOKEN", false)]
        public void IsPasswordGrantType_ReturnsExpectedResult(string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest
            {
                GrantType = type
            };

            // Act and assert
            Assert.Equal(result, request.IsPasswordGrantType());
        }

        [Fact]
        public void IsRefreshTokenGrantType_ThrowsAnExceptionForNullRequest()
        {
            // Arrange
            var request = (OpenIdConnectRequest) null;

            // Act and assert
            var exception = Assert.Throws<ArgumentNullException>(delegate
            {
                request.IsRefreshTokenGrantType();
            });

            Assert.Equal("request", exception.ParamName);
        }

        [Theory]
        [InlineData(null, false)]
        [InlineData("unknown", false)]
        [InlineData("authorization_code", false)]
        [InlineData("client_credentials", false)]
        [InlineData("password", false)]
        [InlineData("refresh_token", true)]
        [InlineData("refresh_token ", false)]
        [InlineData(" refresh_token", false)]
        [InlineData(" refresh_token ", false)]
        [InlineData("AUTHORIZATION_CODE", false)]
        [InlineData("CLIENT_CREDENTIALS", false)]
        [InlineData("PASSWORD", false)]
        [InlineData("REFRESH_TOKEN", false)]
        public void IsRefreshTokenGrantType_ReturnsExpectedResult(string type, bool result)
        {
            // Arrange
            var request = new OpenIdConnectRequest
            {
                GrantType = type
            };

            // Act and assert
            Assert.Equal(result, request.IsRefreshTokenGrantType());
        }
    }
}
