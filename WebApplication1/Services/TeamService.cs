using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
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
            var team =  await _mainDbContext.Teams.FindAsync(TeamID);
            if (team == null)
            {
                throw new FileNotFoundException();
            }
            var org =  await _mainDbContext.Organizations.FindAsync(team.OrganizationID);
            IEnumerable<Member> members =  await _mainDbContext.Members
                .Where(e => _mainDbContext.Memberships.Where ( f => f.MemberID== e.MemberID).Select(e => e.MemberID).Contains(e.MemberID))
                .ToListAsync();
            return new TeamModelDTO
            {
                TeamName = team.TeamName,
                OrganizationName = org.OrganizationName,
                TeamDescription = team.TeamDescription,
                Members = members.ToList()
            };
        }

        public async Task<int> AddMember(int MemberId, int TeamID)
        {
            var Membership = new Membership {MemberID = MemberId, TeamID = TeamID};
            var entity = _mainDbContext.Attach(Membership);
            if ( _mainDbContext.Teams.Find(TeamID).OrganizationID !=
                _mainDbContext.Members.Find(MemberId).OrganizationID)
                return 2;
            await _mainDbContext.SaveChangesAsync();
            return 0;
        }
        public async Task<bool> IfTeam(int TeamID)
        {
            return await _mainDbContext.Teams.Where(e=> e.TeamID == TeamID).AnyAsync();
        }
    }
}