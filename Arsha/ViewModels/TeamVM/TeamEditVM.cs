using Arsha.Models;

namespace Arsha.ViewModels.TeamVM
{
    public class TeamEditVM
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Bio { get; set; } 
        public string? ProfileImageName { get; set; } 
        public int WorkerId { get; set; }
        public IFormFile? Image { get; set; } 
        public List<Worker>? Workers { get; set; }


    }
}
