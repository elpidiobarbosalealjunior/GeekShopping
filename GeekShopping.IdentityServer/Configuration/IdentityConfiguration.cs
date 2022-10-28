using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace GeekShopping.IdentityServer.Configuration;

public static class IdentityConfiguration
{
    public const string Admin = "Admin";
    public const string Client = "Client";

    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new List<ApiResource>
        {
            new ApiResource("geek_shopping.api" , "Geek Shopping API")
            {
                Scopes = new List<string>{ "geek_shopping.read", "geek_shopping.write", "geek_shopping.delete", "profile", "openid" },
                ApiSecrets = new List<Secret>{ new Secret("0cdoci5F@".Sha256()) }
            }
        };
    }

    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };
    }

    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
        {
            new ApiScope("geek_shopping.read"),
            new ApiScope("geek_shopping.write"),
            new ApiScope("geek_shopping.delete"),
        };
    }

    public static IEnumerable<Client> Clients => new List<Client>
    {
        new Client
        {
            ClientId = "gks.client",
            ClientName = "Client Credentials Client",
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            ClientSecrets = { new Secret("0cdoci5f".Sha256()) },
            AllowedScopes = { "geek_shopping.read", "geek_shopping.write", "geek_shopping.delete" }
        },

        new Client
        {
            ClientId = "gks.web",
            ClientSecrets = { new Secret("0cdoci5f".Sha256()) },
            AllowedGrantTypes = GrantTypes.Code,                    
            RedirectUris = { "https://localhost:4430/signin-oidc" },
            PostLogoutRedirectUris = { "https://localhost:4430/signout-callback--oidc" },
            AllowedScopes = { "openid", "profile", "geek_shopping.read", "geek_shopping.write", "geek_shopping.delete" },
            AllowAccessTokensViaBrowser = true
        }
    };
}