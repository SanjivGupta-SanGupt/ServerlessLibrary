using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Octokit;
using Octokit.Internal;
using Microsoft.AspNetCore.Http;

namespace ServerlessLibrary
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class ContributionController : Controller
    {
        ILibraryStore libraryStore;
        public ContributionController(ILibraryStore libraryStore)
        {
            this.libraryStore = libraryStore;
        }

        [HttpGet]
        [Route("Login")]
        public IActionResult Login(string returnUrl = "/")
        {
            if (User.Identity.IsAuthenticated)
            {
                return new RedirectResult(returnUrl);
            }

            return Challenge(new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        [HttpGet]
        [Route("AccountName")]
        public string GetAccountName()
        {
            if (User.Identity.IsAuthenticated)
            {
                var login = ((ClaimsIdentity)User.Identity).FindFirst("urn:github:login").Value;
                return login;
            }
            else
                throw new UnauthorizedAccessException();
        }

        // PUT api/<controller>/
        [HttpPut()]
        public IActionResult Put([FromBody]LibraryItem libraryItem)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrWhiteSpace(ServerlessLibrarySettings.CosmosEndpoint))
                {
                    this.libraryStore.Add(libraryItem);
                }

                return new JsonResult(true);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
