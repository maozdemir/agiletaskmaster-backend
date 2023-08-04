using AutoMapper;
using AgileTaskMaster.DTOs;
using AgileTaskMaster.Models;
using AgileTaskMaster.Repositories;

namespace AgileTaskMaster.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public TeamService(ITeamRepository teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<List<TeamDTO>> GetAllTeams()
        {
            var teams = await _teamRepository.GetAllTeams();
            return _mapper.Map<List<TeamDTO>>(teams);
        }

        public async Task<TeamDTO> GetTeamById(string teamId)
        {
            var team = await _teamRepository.GetTeamById(teamId);
            return _mapper.Map<TeamDTO>(team);
        }

        public async Task<TeamDTO> CreateTeam(TeamDTO teamDTO)
        {
            var team = _mapper.Map<Team>(teamDTO);

            var createdTeam = await _teamRepository.CreateTeam(team);

            return _mapper.Map<TeamDTO>(createdTeam);
        }

        public async Task<TeamDTO> UpdateTeam(TeamDTO teamDTO)
        {
            var existingTeam = await _teamRepository.GetTeamById(teamDTO.Id);
            if (existingTeam == null)
                return null;

            var teamToUpdate = _mapper.Map(teamDTO, existingTeam);

            var updatedTeam = await _teamRepository.UpdateTeam(teamToUpdate);

            return _mapper.Map<TeamDTO>(updatedTeam);
        }

        public async Task<bool> DeleteTeam(string teamId)
        {
            var existingTeam = await _teamRepository.GetTeamById(teamId);
            if (existingTeam == null)
                return false;

            var deleted = await _teamRepository.DeleteTeam(teamId);

            return deleted;
        }

        public async Task<bool> AddUserToTeam(string teamId, string userId)
        {
            var added = await _teamRepository.AddUserToTeam(teamId, userId);
            return added;
        }

        public async Task<bool> RemoveUserFromTeam(string teamId, string userId)
        {
            var removed = await _teamRepository.RemoveUserFromTeam(teamId, userId);
            return removed;
        }
    }
}
