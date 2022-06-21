using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/teams")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamdbService;
        public TeamController(ITeamService teamdbService)
        {
            _teamdbService = teamdbService;
        }

        public async Task<IActionResult> GetTeam(int TeamID)
        {
            try
            {
                var result = await _teamdbService.GetTeam(TeamID);
                if (!_teamdbService.IfTeam(TeamID))
                    return NotFound("team not found");
                return Ok(result);
            }
            catch (SqlException)
            {
                return ;
            }
        }
        public async Task<ActionResult> AddMember(Member MemberID)
        {
            var result = await _teamdbService.AddMember(MemberID);
            switch (result)
            {
                case 0: return Ok();
                case 1: return NotFound("Member not found");
                case 2: return BadRequest("Member is not part of organisation");
                default: return Unauthorized();
            }
        }
    }
}