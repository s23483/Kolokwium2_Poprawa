using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Services
{
    public class TeamService : ITeamService
    {
        private readonly MainDbContext _mainDbContext;

        public TeamService(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<TeamModelDTO> GetTeam(int TeamID)
        {
            
        }

        public async Task<int> AddMember(Member member)
        {
            
        }
    }
}