namespace Arsha.Models
{    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Bio { get; set; } = null!;
        public string ProfileImageName { get; set; } = null!;
        public int WorkerId { get; set; }
        public Worker Worker { get; set; } = null!;
    }

    
}
