using AutoMapper;
using Christmas.Areas.Admin.ViewModels.About;
using Christmas.Data;
using Christmas.Helpers.Extentions;
using Christmas.Models;
using Christmas.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Services
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public TeamService(AppDbContext context,
                            IMapper mapper,
                            IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task CreateAsync(TeamCreateVM team)
        {
            string fileName = $"{Guid.NewGuid()}-{team.Photo.FileName}";

            string path = _env.GetFilePath("img/team", fileName);

            var data = _mapper.Map<Team>(team);

            data.Image = fileName;

            await _context.AddAsync(data);

            await _context.SaveChangesAsync();

            await team.Photo.SaveFile(path);
        }

        public async Task DeleteAsync(int id)
        {
            Team team = await _context.Teams.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            string path = _env.GetFilePath("img/team/", team.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(TeamEditVM request)
        {
            string oldPath = _env.GetFilePath("img/team/", request.Image);

            string fileName = $"{Guid.NewGuid()} - {request.Photo.FileName}";

            string newPath = _env.GetFilePath("img/team/", fileName);

            Team dbTeam = await _context.Teams.AsNoTracking().FirstOrDefaultAsync(m => m.Id == request.Id);


            _mapper.Map(request, dbTeam);

            dbTeam.Image = fileName;

            _context.Teams.Update(dbTeam);
            await _context.SaveChangesAsync();



            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            await request.Photo.SaveFile(newPath);
        }

        public async Task<List<TeamVM>> GetAllAsync()
        {
            var teams = await _context.Teams.ToListAsync();

            return _mapper.Map<List<TeamVM>>(teams);
        }

        public async Task<TeamVM> GetByIdAsync(int id)
        {
            return _mapper.Map<TeamVM>(await _context.Teams.FirstOrDefaultAsync(m => m.Id == id));
        }
    }
}
