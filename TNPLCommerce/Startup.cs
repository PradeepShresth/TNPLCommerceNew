using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TNPLCommerce.Infrastructure.Data.SqlHandlers;
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using TNPLCommerce.Domain.UserModels;
using System.Net;
using TNPLCommerce.Domain.Models;
using TNPLCommerce.Web.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using TNPLCommerce.Application.Interfaces;
using TNPLCommerce.Application.Services;

namespace TNPLCommerce
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration )
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            SQLHandler.Connectionconfig = _configuration.GetSection("ConnectionStrings").GetSection("TNPLConnectionString").Value;

            services.AddScoped<IUserServices, UserServices>();

            #region "USER AUTHENTICATION AND AUTHORIZATION"

            SiteKeys.Configure(_configuration.GetSection("Jwt"));

            var Key = Encoding.ASCII.GetBytes(SiteKeys.Key);

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(token =>
                {
                    token.RequireHttpsMetadata = false;
                    token.SaveToken = true;
                    token.TokenValidationParameters = JwtHelper.tokenValidationParameters;
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                options.AddPolicy(Policies.User, Policies.UserPolicy());
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCookiePolicy();
            app.UseSession();
            app.Use(async (context, next) =>
            {
                string JWToken = context.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(JWToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                    JwtHelper.AttatchUserToContext(JWToken, context);
                }
                await next();
            });
            app.UseAuthentication();


            #region "Redirect if unauthorized or forbidden"  
            
            app.UseStatusCodePages(context => {
                var response = context.HttpContext.Response;
                if (response.StatusCode == (int)HttpStatusCode.Unauthorized ||
                    response.StatusCode == (int)HttpStatusCode.Forbidden)
                    response.Redirect("/User/Login");
                return Task.CompletedTask;
            });

            app.UseAuthorization();

            #endregion


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                                    name: "default",
                                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

}
