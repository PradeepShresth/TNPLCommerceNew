# region "USING STATEMENTS"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNPLCommerce.Domain.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using TNPLCommerce.Domain.Models;
using TNPLCommerce.Application.Services;
using TNPLCommerce.Application.Interfaces;

#endregion

namespace TNPLCommerce.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(User userInfo)
        {
            if (ModelState.IsValid)
            {
                //string userRole = Policies.User;
                //if (HttpContext.Items["User"] != null)
                //{
                //    User userObj = (User)HttpContext.Items["User"];
                //    userRole = userObj.UserRole;
                //} else
                //{
                //    userRole = "User";
                //}
                User user = _userServices.GetUserByEmail(userInfo);

                if (user != null)
                {
                    ViewData["EmailAlreadyExistsMessage"] = "Email already exists.";
                    return View(userInfo);
                }

                //userService.RegisterUser(user, userRole);
                return RedirectToAction("Login", new { message = "You are Successfully Registered!!" });
            }
            return View(userInfo);
        }

        [AllowAnonymous]
        public IActionResult Login(string message)
        {

            if (message != null)
            {
                ViewData["Message"] = message;
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(User userInfo)
        {
            User user = _userServices.GetUserByEmail(userInfo);
            user = _userServices.LoginUser(userInfo);
            if (user != null)
            {
                GenerateJWTToken(user);
                return Redirect("/");
            }

            ViewData["IncorrectData"] = "Email or Password does not match.";
            return View();
        }

        private void GenerateJWTToken(User userInfo)
        {
            var key = Encoding.ASCII.GetBytes(SiteKeys.Key);

            var claims = new[]
            {
                new Claim("Id", userInfo.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Email),
                new Claim("Role",userInfo.UserRole),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var JWToken = new JwtSecurityToken(
                issuer: SiteKeys.Issuer,
                audience: SiteKeys.Audience,
                claims: claims,
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddMinutes(30)).DateTime,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );

            var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
            HttpContext.Session.SetString("JWToken", token);
            HttpContext.Response.Cookies.Append("access_token", token, new CookieOptions { HttpOnly = true });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Route("GetUserData")]
        [Authorize(Policy = Policies.User)]
        public IActionResult GetUserData()
        {
            return Ok("This is a response from user method");
        }
        [HttpGet]
        [Route("GetAdminData")]

        [Authorize(Policy = Policies.Admin)]
        public string GetAdminData()
        {
            return "Response from Admin";
        }
    }
}