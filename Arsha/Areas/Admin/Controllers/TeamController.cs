using Arsha.DataContext;
using Arsha.Models;
using Arsha.ViewModels.TeamVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;

namespace Arsha.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="SuperAdmin")]
    public class TeamController:Controller
    {
        public readonly ArshaDbContext _arshaDbContext;
        public readonly IWebHostEnvironment _webHostEnvironment;
        public TeamController(ArshaDbContext arshaDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _arshaDbContext = arshaDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            List<Team> team= await _arshaDbContext.Teams.ToListAsync();
            return View(team);
        }

        public async Task<IActionResult> Create()
        {
            TeamCreateVM teamCreateVM = new TeamCreateVM()
            {
                Workers = await _arshaDbContext.Workers.ToListAsync(),
            };
        
            return View(teamCreateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TeamCreateVM newteam)
        {
            if(!ModelState.IsValid) 
            {
                newteam.Workers=await _arshaDbContext.Workers.ToListAsync();
                return View(newteam);
            }
            Team team = new Team()
            {
                Bio = newteam.Bio,
                Name = newteam.Name,
                Surname = newteam.Surname,
                WorkerId = newteam.WorkerId,
            };
            if (newteam.Image.ContentType.Contains("image/") && newteam.Image.Length / 1024 > 2048)
            {
                newteam.Workers= await _arshaDbContext.Workers.ToListAsync();
                ModelState.AddModelError("Image", "Error");
                return View(newteam);
            }
            string newFileName = Guid.NewGuid().ToString() + newteam.Image.FileName;
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "team", newFileName);
            using (FileStream stream = new FileStream(path, FileMode.CreateNew))
            {
                newteam.Image.CopyToAsync(stream);
            }
            team.ProfileImageName = newFileName;
            await _arshaDbContext.Teams.AddAsync(team);
            await _arshaDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
       public async Task<IActionResult>Delete(int id)
       {
            Team team =await _arshaDbContext.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
             string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "team", team.ProfileImageName);
            {
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            _arshaDbContext.Teams.Remove(team);
            await _arshaDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
       }

        public async Task<IActionResult> Edit(int id)
        {
            Team team = await _arshaDbContext.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            TeamEditVM teamVM = new TeamEditVM()
            {
                ProfileImageName = team.ProfileImageName,
                Workers = await _arshaDbContext.Workers.ToListAsync(),
                Bio = team.Bio,
                Surname = team.Surname,
                WorkerId = team.WorkerId
            };
            return View(teamVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TeamEditVM newTeam)
        {
            Team? team = await _arshaDbContext.Teams.FindAsync(id);
            if (team == null) { return NotFound(); }
            if(!ModelState.IsValid)
            {
                newTeam.Workers= await _arshaDbContext.Workers.ToListAsync();
                return View(newTeam);
            }
            if (newTeam.Image == null!)
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "team", team.ProfileImageName);
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string newFileName = Guid.NewGuid().ToString() + newTeam.Image.FileName;
                using (FileStream stream= new FileStream(path,FileMode.CreateNew))
                {
                    await newTeam.Image.CopyToAsync(stream);
                }
                team.ProfileImageName = newFileName;
            } 
                team.Surname = newTeam.Surname;
                team.Name= newTeam.Name;
                team.WorkerId= newTeam.WorkerId;
                team.Bio = newTeam.Bio;

            
            await _arshaDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
