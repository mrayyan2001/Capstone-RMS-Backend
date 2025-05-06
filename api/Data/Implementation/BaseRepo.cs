using api.Data.interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace api.Data.Implementation
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class, new()
    {

        private readonly FoodtekDbContext _context;

        public BaseRepo(FoodtekDbContext context)
        {
            _context = context;
        }

        

        public async Task<T?> GetElementByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
        public async Task<bool> IsExistsELementAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id)!=null;
        }
        public async Task<T?> AddElementAsync(T input)
        {
            await _context.Set<T>().AddAsync(input);
            //Check if its tracked
            await _context.AddAsync(input);
            await _context.SaveChangesAsync();
            return input;
        }
        public async Task<IEnumerable<T>?> FindAllAsync(Expression<Func<T,bool>> match)
        {
            return await _context.Set<T>().AsQueryable().Where(match).ToListAsync();
        }
        public async Task<IEnumerable<T>?> FindAllAsync(Expression<Func<T, bool>> match, string[] includes)
        {
            var query=_context.Set<T>().AsQueryable();
            foreach (var item in includes)
            {
                query=query.Include(item);
            }
            return await query.Where(match).ToListAsync();
        }
        public async Task<T?>FindAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().AsQueryable().SingleOrDefaultAsync(match);
        }
        public async Task<T?> FindAsync(Expression<Func<T, bool>> match, string[] includes)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var item in includes)
            {
                query = query.Include(item);
            }
            return await query.SingleOrDefaultAsync(match);
        }
        public async Task<bool> SoftDeleteElementAsync(int id)
        {
            T targetElement = await _context.Set<T>().FindAsync(id);
            var IsActiveProp= targetElement.GetType().GetProperty("IsActive");
            if (IsActiveProp != null)
            {
                IsActiveProp.SetValue(typeof(T), 0);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task<bool> ReactivateElementAsync(int id)
        {
            T targetElement = await _context.Set<T>().FindAsync(id);
            var IsActiveProp = targetElement.GetType().GetProperty("IsActive");
            if (IsActiveProp != null)
            {
                IsActiveProp.SetValue(typeof(T), 1);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public async Task<bool> HardDeleteElementAsync(int id)
        {
            T targetElement = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(targetElement);
            return true;
        }
        public async Task<IEnumerable<T>> GetAllElementAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

    }
}
