using System.Collections.Generic;
using System.Threading.Tasks;
using AgileTaskMaster.Models;
using MongoDB.Driver;

namespace AgileTaskMaster.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly IMongoCollection<Team> _teamCollection;

        public TeamRepository(IMongoDatabase database)
        {
            _teamCollection = database.GetCollection<Team>("teams");
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await _teamCollection.Find(team => true).ToListAsync();
        }

        public async Task<Team> GetTeamById(string teamId)
        {
            return await _teamCollection.Find(team => team.Id == teamId).FirstOrDefaultAsync();
        }

        public async Task<Team> CreateTeam(Team team)
        {
            await _teamCollection.InsertOneAsync(team);
            return team;
        }

        public async Task<Team> UpdateTeam(Team team)
        {
            var updateResult = await _teamCollection.ReplaceOneAsync(t => t.Id == team.Id, team);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0 ? team : null;
        }

        public async Task<bool> DeleteTeam(string teamId)
        {
            var deleteResult = await _teamCollection.DeleteOneAsync(team => team.Id == teamId);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<bool> AddUserToTeam(string teamId, string userId)
        {
            var team = await _teamCollection.Find(t => t.Id == teamId).FirstOrDefaultAsync();
            if (team == null)
                return false;

            var updateResult = await _teamCollection.ReplaceOneAsync(t => t.Id == teamId, team);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> RemoveUserFromTeam(string teamId, string userId)
        {
            var team = await _teamCollection.Find(t => t.Id == teamId).FirstOrDefaultAsync();
            if (team == null)
                return false;
            var updateResult = await _teamCollection.ReplaceOneAsync(t => t.Id == teamId, team);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
