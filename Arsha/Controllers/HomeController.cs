
using Arsha.DataContext;
using Arsha.Models;
using Arsha.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Arsha.Controllers
{
    public class HomeController : Controller
    {
        private readonly ArshaDbContext _arshaDbContext;
        public HomeController(ArshaDbContext arshaDbContext)
        {
            _arshaDbContext = arshaDbContext;
        }
        

        public async Task<IActionResult> Index()
        {
            List<Team> teams= await _arshaDbContext.Teams.Include(c=>c.Worker).ToListAsync();
            HomeVM homeVM = new HomeVM()
            {
                Teams = teams
            };
            return View(homeVM);
        }

        
        

    }

}