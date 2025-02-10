using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Nexus.Auth.Domain.Entities;
using Nexus.Auth.Infra.Context;
using Nexus.Auth.Repository.Dtos.Generics;
using Nexus.Auth.Repository.Services.Interfaces;
using System.Globalization;

namespace Nexus.Auth.Repository.Services
{
    public class UserService : IUserService<User>
    {
        private readonly UserManager<User> _userManager;
        private readonly IRoleService<Role> _roleService;
        private readonly NexusAuthContext _context;

        public UserService(UserManager<User> userManager, IRoleService<Role> roleService, NexusAuthContext context)
        {
            _userManager = userManager;
            _roleService = roleService;
            _context = context;
        }

        public async Task<List<User>> GetAllAsync(PageParams pageParams, int placeId)
        {
            var query = _userManager.Users.AsQueryable();

            string term = pageParams.Term.ToLower();
            bool isDateTerm = DateTime.TryParseExact(term, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate);

            query = query.Where(x =>
                x.PlaceData == placeId &&
                (x.Name.ToLower().Contains(term) ||
                x.UserName.ToLower().Contains(term) ||
                x.Email.ToLower().Contains(term) ||
                (isDateTerm && x.ChangeDate.Date == parsedDate.Date) ||
                x.ChangeDate.Year.ToString().Contains(term) ||
                x.ChangeDate.Month.ToString().Contains(term) ||
                x.ChangeDate.Day.ToString().Contains(term) ||
                x.UserPlaces.Any(x => x.Place.Name.ToLower().Contains(term)))
            )
            .Include(x => x.UserRoles)
            .ThenInclude(x => x.Role)
            .Include(y => y.UserPlaces)
            .ThenInclude(y => y.Place);


            if (!string.IsNullOrEmpty(pageParams.OrderByProperty))
            {
                switch (pageParams.OrderByProperty.ToLower())
                {
                    case "name":
                        query = pageParams.Asc ? query.OrderBy(u => u.Name) : query.OrderByDescending(u => u.Name);
                        break;
                    case "username":
                        query = pageParams.Asc ? query.OrderBy(u => u.UserName) : query.OrderByDescending(u => u.UserName);
                        break;
                    case "email":
                        query = pageParams.Asc ? query.OrderBy(u => u.Email) : query.OrderByDescending(u => u.Email);
                        break;
                    case "changedate":
                        query = pageParams.Asc ? query.OrderBy(u => u.ChangeDate) : query.OrderByDescending(u => u.ChangeDate);
                        break;
                    case "places":
                        query = pageParams.Asc 
                            ? query.OrderBy(u => u.UserPlaces.OrderBy(y => y.Place.Name).FirstOrDefault().Place.Name) 
                            : query.OrderByDescending(u => u.UserPlaces.OrderBy(y => y.Place.Name).FirstOrDefault().Place.Name);
                        break;
                    default:
                        query = query.OrderBy(u => u.Id);
                        break;
                }
            }
            else
                query = query.OrderBy(u => u.Id);
            
            var items = await query.Skip((pageParams.PageNumber - 1) * pageParams.PageSize).Take(pageParams.PageSize).ToListAsync();
            return items;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var query = _userManager.Users.AsQueryable();
            
            query = query
                .Where(u => u.Id == id)
                .Include(x => x.UserRoles)
                .ThenInclude(x => x.Role)
                .Include(y => y.UserPlaces);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<User> GetByNameAsync(string name)
        {
            //var res = await _userManager.FindByNameAsync(name);
            return await _context.Users.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<bool> Add(User entity)
        {
            var result = await _userManager.CreateAsync(entity);
            return result.Succeeded;
        }

        public async Task<IList<Role>> AddRoles(List<UserRole> list)
        {
            await _context.UserRoles.AddRangeAsync(list);

            if (await SaveChangesAsync())
            {
                var userId = list.FirstOrDefault().UserId;

                if (userId > 0)
                    return await _roleService.GetByUserIdAsync(userId);
            }

            throw new Exception("Error saving or loaging entity");
        }

        public async Task<bool> Update(User entity)
        {
            var result = await _userManager.UpdateAsync(entity);
            return result.Succeeded;
        }

        public async Task<bool> ChangeStatus(User entity, bool status)
        {
            entity.Blocked = status;
            var result = await _userManager.UpdateAsync(entity);
            return result.Succeeded;
        }

        public async Task<bool> Delete(int id)
        {
            var User = await GetByIdAsync(id);
            if (User is not null)
            {
                var result = await _userManager.DeleteAsync(User);
                return result.Succeeded;
            }

            return false;
        }

        public async Task<bool> DeleteRoles(int userId)
        {
            var users = await _context.UserRoles.Where(_ => _.UserId == userId).ToListAsync();

            if (users.Count() > 0)
            {
                _context.UserRoles.RemoveRange(users);
                return await SaveChangesAsync();
            }

            return false;

            throw new Exception("Error saving or loaging entity");
        }

        public Task<bool> DeleteRange(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        {
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> RegisterRoles(User entity)
        {
            return await _userManager.UpdateAsync(entity);
        }

        public async Task<IList<string>> GetRolesByIdAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<IdentityResult> RemoveRoles(User user, IList<string> userRoles)
        {
            return await _userManager.RemoveFromRolesAsync(user, userRoles);
        }

        public async Task<List<UserPlace>> GetPlacesByUserId(int userId)
        {
            return await _context.UserPlaces
                .Where(_ => _.UserId == userId)
                .Include(p => p.Place)
                .ToListAsync();
        }
    }
}
