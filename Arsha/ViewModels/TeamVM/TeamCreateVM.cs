using Arsha.Models;
using System.ComponentModel.DataAnnotations;

namespace Arsha.ViewModels.TeamVM
{
    public class TeamCreateVM
    {
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Bio { get; set; } = null!;
        public int WorkerId { get; set; }
        public IFormFile Image { get; set; }=null!;
        public List<Worker>? Workers { get; set; }

    }
}
