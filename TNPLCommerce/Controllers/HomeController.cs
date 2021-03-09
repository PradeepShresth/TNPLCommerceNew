using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TNPLCommerce.Application.Services;
using TNPLCommerce.Domain.UserModels;

namespace TNPLCommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            if (HttpContext.Items["User"] != null)
            {
                User userObj = (User)HttpContext.Items["User"];
                return View(userObj);
            }
            return View();
        }
    }
}
