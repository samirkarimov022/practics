﻿using Microsoft.AspNetCore.Mvc;

namespace Cars.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
