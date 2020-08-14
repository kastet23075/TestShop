using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestShop.Web.Controllers
{
    public class BaseController : Controller
    {
        protected string UserId => User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}