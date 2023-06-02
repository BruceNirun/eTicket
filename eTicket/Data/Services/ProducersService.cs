using eTicket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace eTicket.Data.Services
{
    public class ProducerService : IProducersService
    {
        private readonly AppDbcontext _context;
        public ProducerService(AppDbcontext context)
        {
            _context = context;
        }
        async Task IProducersService.AddAsync(Producer producer)
        {
            await _context.Producers.AddAsync(producer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.Producers.FirstOrDefaultAsync(s => s.Id == id);
            _context.Producers.Remove(result);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Producer>> GetAllAsync()
        {
            var result = await _context.Producers
                .ToListAsync();
            return result;
        }

        public async Task<Producer> GetByIdAsync(int id)
        {
            var result = await _context.Producers .FirstOrDefaultAsync(s => s.Id == id);
            return result;
        }

        public async Task<Producer> UpdateAsync(int id, Producer newProducer)
        {
            _context.Update(newProducer);
            await _context.SaveChangesAsync();
            return newProducer;
        }
    }
}
