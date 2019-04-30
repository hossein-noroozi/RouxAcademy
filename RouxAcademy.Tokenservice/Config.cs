using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouxAcademy.Tokenservice
{
    public class Config
    {

        /// <summary>
        /// a collection of Identity Claims For Reaching a resource // you can Add default and custome claims to it
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResource()
                {
                    Name = "Role",
                    UserClaims = new List<string>
                    {"Role" }
                }
            };
        }


        /// <summary>
        /// difinig our clients for security and options needed for client security
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {

            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "RouxAcademyMvc",
                    ClientName = "Roux Academy Mvc Client",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = {"http://localhost5002/signin-oidc" },
                    PostLogoutRedirectUris={"http://localhost5002/signout-callback-oicd" },
                    AllowedScopes = new List<string>()
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email
                    }
                }
            };
        }


    }
}
