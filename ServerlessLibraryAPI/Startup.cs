using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Security.Claims;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Rewrite;

namespace ServerlessLibrary
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddMvc();
            services.AddCors();
            services.AddCors(options => options.AddPolicy("AllowAll", p => p.WithOrigins("http://localhost:8080")
  .AllowAnyMethod().AllowCredentials()
  .AllowAnyHeader()));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "GitHub";
            })
              .AddCookie()
   
              .AddOAuth("GitHub", options =>
              {
                  options.ClientId = "";
                  options.ClientSecret = "";
                  options.CallbackPath = new PathString("/signin-github");

                  options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
                  options.TokenEndpoint = "https://github.com/login/oauth/access_token";
                  options.UserInformationEndpoint = "https://api.github.com/user";
                  options.Scope.Add("user");
                  options.Scope.Add("repos");

                  options.SaveTokens = true;

                  options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                  options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                  options.ClaimActions.MapJsonKey("urn:github:login", "login");
                  options.ClaimActions.MapJsonKey("urn:github:url", "html_url");
                  options.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");

                  options.Events = new OAuthEvents
                  {
                      OnCreatingTicket = async context =>
                      {
                          var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
                          request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                          request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
                          Claim claimA = new Claim("Bearer", context.AccessToken);
                          context.Identity.AddClaim(claimA);
                          var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
                          response.EnsureSuccessStatusCode();

                          var user = JObject.Parse(await response.Content.ReadAsStringAsync());

                         context.RunClaimActions(user);
                      },
                      
                  };
              });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "ASP.NET Core 2.0 Web API",
                    Version = "v1"
                });
            });
            //#region snippet_ConfigureApiBehaviorOptions
            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.SuppressConsumesConstraintForFormFileParameters = true;
            //    options.SuppressInferBindingSourcesForParameters = true;
            //    options.SuppressModelStateInvalidFilter = true;
            //});
            //#endregion
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<ILibraryStore, CosmosLibraryStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var options = new RewriteOptions()
           .AddRedirect("Authenticated", "?type=logicapp");
            app.UseRewriter(options);
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Serverless library API v1");
                c.RoutePrefix = "swagger";
            });
           
            app.UseAuthentication();
            app.UseMvc();
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");
                context.Response.Headers.Add("Strict-Transport-Security", "max-age=600; includeSubDomains; preload");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                await next();
            });
            app.UseCors("AllowAll");
        }
    }
}
