using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RockwellBlog.Data;
using RockwellBlog.Models;
using RockwellBlog.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace RockwellBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IBlogFileService _fileService;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IBlogFileService fileService)
        {
            _logger = logger;
            _context = context;
            _fileService = fileService;
        }

        public async Task<IActionResult> Index(int? page)
        {
            ViewData["HeaderText"] = "The Landing Page";
            ViewData["SubText"] = "Welcome to my landing page";

            var pageNumber = page ?? 1;
            var pageSize = 1;


            var allBlogs = await _context.Blogs.OrderByDescending(b => b.Created)
                                               .ToPagedListAsync(pageNumber, pageSize);
            
            return View(allBlogs);
        }

        public IActionResult Privacy()
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