using System.Collections.Generic;
using System.Threading.Tasks;
using AgileTaskMaster.Models;

namespace AgileTaskMaster.Repositories
{
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> GetAllTeams();
        Task<Team> GetTeamById(string teamId);
        Task<Team> CreateTeam(Team team);
        Task<Team> UpdateTeam(Team team);
        Task<bool> DeleteTeam(string teamId);
        Task<bool> AddUserToTeam(string teamId, string userId);
        Task<bool> RemoveUserFromTeam(string teamId, string userId);
    }
}
