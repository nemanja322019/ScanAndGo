using DataLibrary.Data;
using DataLibrary.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.DtoModels;
using ModelsLibrary.Enums;
using ModelsLibrary.Models;

namespace DataLibrary.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<User> Add(User user)
        {
            _dbContext.users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }
        public async Task Delete(User user)
        { 
            _dbContext.users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<User>> GetAll()
        {
            var users = await _dbContext.users.ToListAsync();
            return users;
        }

        public async Task<PageDto<User>> GetAll(int pageNumber, int pageSize)
        {
            var query = _dbContext.users.Include(x => x.WorkingInStore).AsQueryable();
            var totalItems = await query.CountAsync();
            var data = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PageDto<User>(data, totalItems);

        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _dbContext.users.Include(x => x.OwnedStores)
                    .Include(x => x.WorkingInStore)        
                    .FirstOrDefaultAsync(a => a.Email == email);     
        }
        public async Task<User?> GetUserByEmailNoTrack(string email)
        {
            return await _dbContext.users.AsNoTracking()
                .FirstOrDefaultAsync(a => a.Email == email);
        }
        public async Task<User?> GetUserById(int id)
        {
            return await _dbContext.users.Include(x => x.OwnedStores)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public void Save(User user)
        {
            if (!_dbContext.ChangeTracker.Entries<User>().Any(e => e.Entity == user))
            {
                _dbContext.Entry(user).State = EntityState.Modified;
            }
        }
        public async Task SaveChanges()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task<User> Update(User user)
        {
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAllByUserType(UserTypes type)
        {
           return await _dbContext.users
                   .Where(u => u.UserType == type).ToListAsync();
        }
    }
}
