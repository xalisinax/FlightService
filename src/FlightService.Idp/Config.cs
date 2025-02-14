using Duende.IdentityServer.Models;

namespace FlightService.Idp;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        [
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        ];

    public static IEnumerable<ApiScope> ApiScopes => [];

    public static IEnumerable<Client> Clients =>
        [
            new Client
            {
                ClientId = "flight-client",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = { "http://localhost:5173/callback" },
                FrontChannelLogoutUri = "http://localhost:5173/signout",
                PostLogoutRedirectUris = { "http://localhost:5173/logout" },
                AllowedCorsOrigins = { "http://localhost:5173"},
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile" }
            },
        ];
}
