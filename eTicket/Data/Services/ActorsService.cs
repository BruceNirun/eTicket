using eTicket.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace eTicket.Data.Services
{
    public class ActorsService : IActorsServices
    {
        private readonly AppDbcontext _context;
        public ActorsService(AppDbcontext context)
        {
            _context = context;
        }
        async Task IActorsServices.AddAsync(Actor actor)
        {
            await _context.Actors.AddAsync(actor);
            await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Actor>> GetAllAsync()
        {
            var result = await _context.Actors
                .ToListAsync();
            return result;
        }

        public async Task<Actor> GetByIdAsync(int id)
        {
            var result = await _context.Actors .FirstOrDefaultAsync(s => s.Id == id);
            return result;
        }

        public Actor Update(int id, Actor newActor)
        {
            throw new NotImplementedException();
        }

    }
}
