﻿using System.Web.Mvc;

namespace Univesp.PI1.REST.DiarioEletronico.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}