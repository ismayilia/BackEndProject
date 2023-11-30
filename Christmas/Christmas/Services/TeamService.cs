using AutoMapper;
using Christmas.Data;
using Christmas.Services.Interfaces;
using Christmas.ViewModels.About;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Services
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        

        public TeamService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<TeamVM>> GetAllAsync()
        {
            var teams = await _context.Teams.ToListAsync();

            return _mapper.Map<List<TeamVM>>(teams);
        }
    }
}
