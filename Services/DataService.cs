using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RockwellBlog.Data;
using RockwellBlog.Enums;
using RockwellBlog.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RockwellBlog.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IBlogFileService _fileService;
        private readonly UserManager<BlogUser> _userManager;
        private readonly IConfiguration _configuration;

        public DataService(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, IBlogFileService fileService, UserManager<BlogUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _roleManager = roleManager;
            _fileService = fileService;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task ManageDataAsync()
        {
            //Task 0: Make sure the DB is present by running through the migrations
            await _context.Database.MigrateAsync();

            //Task 1: Seed Roles - Creating Roles and entering them into the system (AspNetRoles)
            await SeedRolesAsync();

            //Task 2: Seed a few users in the system (AspNetUsers)
            await SeedUsersAsync();

        }

        private async Task SeedRolesAsync()
        {
            //Are there any roles already in the system
            if (_context.Roles.Any())
                return;

            foreach (var stringRole in Enum.GetNames(typeof(BlogRole)))
            {
                var identityRole = new IdentityRole(stringRole);
                await _roleManager.CreateAsync(identityRole);
            }
        }

        private async Task SeedUsersAsync()
        {
            if (_context.Users.Any(u => u.Email == "JFlynn@Mailinator.com"))
            {
                return;
            }

            var adminUser = new BlogUser()
            {
                Email = "JFlynn@Mailinator.com",
                UserName = "JFlynn@Mailinator.com",
                FirstName = "John",
                LastName = "Flynn",
                PhoneNumber = "555-1212",
                EmailConfirmed = true,
                ImageData = await _fileService.EncodeFileAsync("JohnImage.jpg"),
                ContentType = "jpg"
            };

            await _userManager.CreateAsync(adminUser, "Abc&123!");
            //await _userManager.CreateAsync(adminUser, _configuration["AdminPassword"]);

            //await _userManager.AddToRoleAsync(adminUser, "Administrator");
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());

            var modUser = new BlogUser()
            {
                Email = "JohnFlynn@Mailinator.com",
                UserName = "JohnFlynn@Mailinator.com",
                FirstName = "John",
                LastName = "Flynn",
                PhoneNumber = "555-1212",
                EmailConfirmed = true,
                ImageData = await _fileService.EncodeFileAsync("JohnImage.jpg"),
                ContentType = "jpg"
            };

            //await _userManager.CreateAsync(adminUser, "Abc&123!");
            await _userManager.CreateAsync(modUser, _configuration["AdminPassword"]);

            //await _userManager.AddToRoleAsync(adminUser, "Administrator");
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());


        }

    }
}