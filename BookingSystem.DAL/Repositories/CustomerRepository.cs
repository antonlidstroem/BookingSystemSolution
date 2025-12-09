using BookingSystem.DAL.Data;
using BookingSystem.DAL.Interface;
using BookingSystem.DAL.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSystem.DAL.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly BookingSystemAPIContext _context;

        public CustomerRepository(BookingSystemAPIContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _context.Customer.ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customer.FindAsync(id);
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customer.AddAsync(customer);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Remove(Customer customer)
        {
            _context.Customer.Remove(customer);
        }

    }
}
