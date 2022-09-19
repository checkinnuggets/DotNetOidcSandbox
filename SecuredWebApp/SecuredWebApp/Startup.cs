using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SecuredWebApp
{
    // Based on:
    //  https://identityserver4.readthedocs.io/en/latest/quickstarts/2_interactive_aspnetcore.html

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();


            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            // add the authentication services to DI.
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = "Cookies";          // We are using a cookie to locally sign-in the user (matching above)
                    options.DefaultChallengeScheme = "oidc";    // and when we need the user to login, we will be using the OpenID Connect protocol.
                })
                .AddCookie("Cookies")                           // add the handler that can process cookies.
                .AddOpenIdConnect("oidc", options =>
                {
                    // configure the handler that performs the OpenID Connect protocol.
                    options.Authority = "https://localhost:44398";      // where the trusted token service is located

                    options.ClientId = "mvc";                           // identify this client to the server
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";
                    
                    // Specify claims to return.  These must be listed for the client
                    options.Scope.Add("profile");
                    options.GetClaimsFromUserInfoEndpoint = true;

                    options.SaveTokens = true;                          // persist the tokens from IdentityServer in the cookie (as they will be needed later).
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Lax
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute()
                    .RequireAuthorization();
            });
        }
    }
}
