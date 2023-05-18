﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using RswebProject.Models;

namespace RswebProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Books()
        {
            return View();
        }
        public IActionResult EditBook()
        {
            return View();
        }
        public IActionResult CreateBook()
        {
            return View();
        }
        public IActionResult DeleteBook()
        {
            return View();
        }
        public IActionResult DetailsBook()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
