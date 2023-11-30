using AutoMapper;
using Christmas.Data;
using Christmas.Services.Interfaces;
using Christmas.ViewModels.About;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Services
{
    public class BrandService : IBrandService
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public BrandService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<BrandVM>> GetAllAsync()
        {
            var brands = await _context.Brands.ToListAsync();

            return _mapper.Map<List<BrandVM>>(brands);
        }

    }
}
