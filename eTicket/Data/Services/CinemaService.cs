using eTicket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace eTicket.Data.Services
{
    public class CinemaService : ICinemasService
    {
        private readonly AppDbcontext _context;
        public CinemaService(AppDbcontext context)
        {
            _context = context;
        }
        public async Task AddAsync(Cinema cinema)
        {
            await _context.Cinemas.AddAsync(cinema);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Cinemas.FirstOrDefaultAsync(s => s.Id == id);
            _context.Cinemas.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Cinema>> GetAllAsync()
        {
            var result = await _context.Cinemas
                .ToListAsync();
            return result;
        }

        public async Task<Cinema> GetByIdAsync(int id)
        {
            var result = await _context.Cinemas.FirstOrDefaultAsync(s => s.Id == id);
            return result;
        }

        public async Task<Cinema> UpdateAsync(int id, Cinema newCinema)
        {
            _context.Update(newCinema);
            await _context.SaveChangesAsync();
            return newCinema;
        }
    }
}
