using eTicket.Models;

namespace eTicket.Data.Services
{
    public interface ICinemasService
    {
        Task<IEnumerable<Cinema>> GetAllAsync();
        Task<Cinema> GetByIdAsync(int id);
        Task AddAsync(Cinema cinema);
        Task<Cinema> UpdateAsync(int id, Cinema newCinema);
        Task DeleteAsync(int id);
    }
}
