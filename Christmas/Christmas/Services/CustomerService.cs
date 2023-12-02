using AutoMapper;
using Christmas.Areas.Admin.ViewModels.Customer;
using Christmas.Data;
using Christmas.Helpers.Extentions;
using Christmas.Models;
using Christmas.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Christmas.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public CustomerService(AppDbContext context,
                             IMapper mapper,
                             IWebHostEnvironment env)
        {
            _context = context;
            _mapper = mapper;
            _env = env;
        }

        public async Task DeleteAsync(int id)
        {
            Customer customer = await _context.Customers.Where(m => m.Id == id).Include(m => m.Reviews).FirstOrDefaultAsync();
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            string path = _env.GetFilePath("img/testimonial", customer.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task<List<CustomerVM>> GetAllAsync()
        {
            return _mapper.Map<List<CustomerVM>>(await _context.Customers.ToListAsync());
        }

        public async Task<CustomerVM> GetByIdAsync(int id)
        {
            return _mapper.Map<CustomerVM>(await _context.Customers.FirstOrDefaultAsync(m => m.Id == id));
        }
    }
}
