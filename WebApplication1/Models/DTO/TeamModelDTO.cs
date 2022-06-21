using System.Collections.Generic;

namespace WebApplication1.Models.DTO
{
    public class TeamModelDTO
    {
        public string TeamName { get; set; }
        public string OrganizationName { get; set; }
        public string TeamDescription { get; set; }
        public  ICollection<string> Members { get; set; }
    }
}