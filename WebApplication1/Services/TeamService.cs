using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
            var member = _mainDbContext.Attach(member);
            
        }
        public async Task<bool> IfTeam(int TeamID)
        {
            return await _mainDbContext.Teams.Where(e=> e.TeamID == TeamID).AnyAsync();
        }
    }
}