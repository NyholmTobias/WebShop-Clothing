// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.


using Duende.IdentityServer.Models;
using System.Collections.Generic;

namespace DreamTeam.IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource(
                    "admin",
                    new [] { "admin" }),
                new IdentityResource(
                    "customer",
                    new [] { "customer" }),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {

                new Client
                {
                    ClientId = "projectclient",
                    ClientName = "Project Client",
                    AllowOfflineAccess = true, //Sätter upp för auto refresh token
                    RequireConsent = false, //Stänger av extra consent page efter login
                    RequirePkce = true,
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = {
                        new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                    RedirectUris = { "https://localhost:44361/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44361/signout-oidc" },
                    AllowedScopes = { "openid", "profile", "email", "admin", "customer"}
                },
                
            };
    }
}