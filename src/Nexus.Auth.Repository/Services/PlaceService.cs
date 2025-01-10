using Microsoft.EntityFrameworkCore;
using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Infra.Context;
using Nexus.Auth.Infra.Services;
using Nexus.Auth.Repository.Services.Interfaces;

namespace Nexus.Auth.Repository.Services
{
    public class PlaceService : BaseDataService<Place>, IPlaceService
    {
        public PlaceService(NexusAuthContext context) : base(context) { }

        public async Task<Place> GetByIdAsync(int id)
        {
            return await _context.Places.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Place> GetByNameAsync(string name)
        {
            return await _context.Places.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<List<Place>> GetByIdsAsync(List<int> ids)
        {
            return await _context.Places.Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public async Task<IList<Place>> GetByUserIdAsync(int userId)
        {
            var userRoles = _context
                .UserPlaces
                .Include(_ => _.Place)
                .Where(x => x.UserId == userId);
            return await userRoles.Where(x => x.Place != null).Select(x => x.Place).ToListAsync();
        }

    }
}
