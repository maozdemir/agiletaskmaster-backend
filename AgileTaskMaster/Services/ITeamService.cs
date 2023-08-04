using System.Collections.Generic;
using System.Threading.Tasks;
using AgileTaskMaster.DTOs;

namespace AgileTaskMaster.Services
{
    public interface ITeamService
    {
        Task<List<TeamDTO>> GetAllTeams();
        Task<TeamDTO> GetTeamById(string teamId);
        Task<TeamDTO> CreateTeam(TeamDTO teamDTO);
        Task<TeamDTO> UpdateTeam(TeamDTO teamDTO);
        Task<bool> DeleteTeam(string teamId);
        Task<bool> AddUserToTeam(string teamId, string userId);
        Task<bool> RemoveUserFromTeam(string teamId, string userId);
    }
}
