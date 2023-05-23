using System.ComponentModel.DataAnnotations;

namespace Arsha.Models
{
    public class Worker
    {
        public Worker() 
        { 
          Teams=new List<Team>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } 
       
        public ICollection<Team> Teams { get; set; }
    }
}
