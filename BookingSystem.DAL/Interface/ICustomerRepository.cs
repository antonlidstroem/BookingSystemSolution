using BookingSystem.DAL.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookingSystem.DAL.Interface
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task AddAsync(Customer customer);
        Task SaveChangesAsync();
        void Remove(Customer customer);
    }
}
