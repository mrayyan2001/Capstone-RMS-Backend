using System.Linq.Expressions;

namespace api.Data.interfaces
{
    public interface IBaseRepo<T> where T : class, new()
    {
        public Task<T?> GetElementByIdAsync(int id);
        public Task<bool> IsExistsELementAsync(int id);
        public Task<T?> AddElementAsync(T input);
        public Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> match);
        //TODO : replace List with expression
        public Task<IEnumerable<T>?> FindAllAsync(Expression<Func<T, bool>> match, string[] includes);
        public Task<T?> FindAsync(Expression<Func<T, bool>> match);
        public Task<T?> FindAsync(Expression<Func<T, bool>> match, string[] includes);
        public Task<bool> ReactivateElementAsync(int id);
        public Task<bool> SoftDeleteElementAsync(int id);
        public  Task<bool> HardDeleteElementAsync(int id);
        public Task<IEnumerable<T>> GetAllElementAsync();
    }
}
