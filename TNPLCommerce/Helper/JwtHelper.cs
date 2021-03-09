using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNPLCommerce.Application.Interfaces;
using TNPLCommerce.Application.Services;
using TNPLCommerce.Domain.Models;
using TNPLCommerce.Domain.UserModels;

namespace TNPLCommerce.Web.Helper
{
    public class JwtHelper
    {
        private static IUserServices _userServices;
        public JwtHelper(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public static TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SiteKeys.Key)),
            ValidateIssuer = true,
            ValidIssuer = SiteKeys.Issuer,
            ValidateAudience = true,
            ValidAudience = SiteKeys.Audience,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        public static void AttatchUserToContext(string JWToken, HttpContext context)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(
                JWToken,
                tokenValidationParameters,
                out SecurityToken validatedToken);

            var Token = (JwtSecurityToken)validatedToken;
            int ID = int.Parse(Token.Claims.First(x => x.Type == "Id").Value);

            // Get User from database
            UserServices userServices = new UserServices();
            User userInfo = userServices.GetUserById(ID);

            // attach account to context on successful jwt validation
            if (userInfo != null)
                context.Items["User"] = userInfo;

            //var test = context.Items["User"];
        }
    }
}
