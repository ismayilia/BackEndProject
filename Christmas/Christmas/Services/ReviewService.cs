using AutoMapper;
using Christmas.Areas.Admin.ViewModels.Review;
using Christmas.Data;
using Christmas.Services.Interfaces;
using Christmas.Areas.Admin.ViewModels.Advert;
using Microsoft.EntityFrameworkCore;
using Christmas.Models;

namespace Christmas.Services
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        

        public ReviewService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int id)
        {
            Review review = await _context.Reviews.Where(m => m.Id == id).FirstOrDefaultAsync();
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ReviewVM>> GetAllAsync()
        {
            var reviews = await _context.Reviews.Include(m=>m.Customer).ToListAsync();

            return _mapper.Map<List<ReviewVM>>(reviews);
        }

        public async Task<ReviewVM> GetByIdWithIncludeAsync(int id)
        {
            return _mapper.Map<ReviewVM>(await _context.Reviews.Include(m => m.Customer).FirstOrDefaultAsync(m => m.Id == id));
        }
    }
}
