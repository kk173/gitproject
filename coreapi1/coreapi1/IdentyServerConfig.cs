using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace coreapi1
{
    public class IdentyServerConfig
    {
        public static IConfiguration Configuration { get; set; }
        /// <summary>
        /// Define which APIs will use this IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("clientservice", "CAS Client Service",new List<string>{ IdentityModel.JwtClaimTypes.Role }),
                new ApiResource("productservice", "CAS Product Service"),
                new ApiResource("agentservice", "CAS Agent Service")
            };
        }

        /// <summary>
        /// Define which Apps will use thie IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "client.api.service",
                    ClientSecrets = new [] { new Secret("clientsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "clientservice" }
                },
                new Client
                {
                    ClientId = "product.api.service",
                    ClientSecrets = new [] { new Secret("productsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "clientservice", "productservice" }
                },
                new Client
                {
                    ClientId = "agent.api.service",
                    ClientSecrets = new [] { new Secret("agentsecret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = new [] { "agentservice", "clientservice", "productservice" }
                }
            };
        }

        /// <summary>
        /// Define which uses will use this IdentityServer
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<TestUser> GetUsers()
        {
            return new List<TestUser>();
            return new[]
            {
                new TestUser
                {
                    SubjectId = "10001",
                    Username = "edison@hotmail.com",
                    Password = "edisonpassword",
                    Claims =new List<Claim>
                    {
                        new Claim(JwtClaimTypes.Role, "superadmin"),
                        new Claim(JwtClaimTypes.Role, "superadmin1"),
                        new Claim(JwtClaimTypes.Role, "superadmin2"),
                        new Claim(JwtClaimTypes.Role, "superadmin3"),
                        new Claim(JwtClaimTypes.Role, "superadmin4"),
                        new Claim(JwtClaimTypes.Role, "superadmin5"),
                        new Claim(JwtClaimTypes.Role, "superadmin6"),
                        new Claim(JwtClaimTypes.Role, "superadmin7"),
                        new Claim(JwtClaimTypes.Role, "superadmin8"),
                        new Claim(JwtClaimTypes.Role, "superadmin9"),
                        new Claim(JwtClaimTypes.Role, "superadmin10"),
                        new Claim(JwtClaimTypes.Role, "superadmin11"),
                        new Claim(JwtClaimTypes.Role, "superadmin12"),
                        new Claim(JwtClaimTypes.Role, "superadmin13"),
                        new Claim(JwtClaimTypes.Role, "superadmin14"),
                        new Claim(JwtClaimTypes.Role, "superadmin15"),
                        new Claim(JwtClaimTypes.Role, "superadmin16"),
                        new Claim(JwtClaimTypes.Role, "superadmin17"),
                    }
                },
                new TestUser
                {
                    SubjectId = "10002",
                    Username = "andy@hotmail.com",
                    Password = "andypassword"
                },
                new TestUser
                {
                    SubjectId = "10003",
                    Username = "leo@hotmail.com",
                    Password = "leopassword"
                }
            };
        }
    }
}
