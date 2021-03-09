//using Microsoft.AspNetCore.Http;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Options;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TNPLCommerce.Application.Services;

//namespace TNPLCommerce.Web
//{
//    public class JwtMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly IConfiguration _config;

//        public JwtMiddleware(RequestDelegate next, IConfiguration config)
//        {
//            _next = next;
//            _config = config;
//        }

//        public async Task Invoke(HttpContext context)
//        {
//            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
//            var token = context.Request.Cookies["access_token"];

//            if (token != null)
//                AttachAccountToContext(context, token);

//            await _next(context);
//        }

//        private void AttachAccountToContext(HttpContext context, string token)
//        {
//            try
//            {
//                var tokenHandler = new JwtSecurityTokenHandler();
//                var key = Encoding.ASCII.GetBytes(_config["Jwt:SecretKey"]);
//                tokenHandler.ValidateToken(token, new TokenValidationParameters
//                {
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(key),
//                    ValidateIssuer = false,
//                    ValidateAudience = false,
//                    set clockskew to zero so tokens expire exactly at token expiration time(instead of 5 minutes later)
//                    ClockSkew = TimeSpan.Zero
//                }, out SecurityToken validatedToken);

//                var jwtToken = (JwtSecurityToken)validatedToken;
//                int UserId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

//                Get User from database
//               UserService userService = new UserService();
//                var userInfo = userService.GetUser(UserId);

//                attach account to context on successful jwt validation
//                if (userInfo != null)
//                    context.Items["User"] = userInfo;

//                var test = context.Items["User"];
//            }
//            catch
//            {
//                do nothing if jwt validation fails
//                 account is not attached to context so request won't have access to secure routes
//            }
//        }
//    }
//}