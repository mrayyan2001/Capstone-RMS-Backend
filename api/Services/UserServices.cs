using api.Data.interfaces;
using api.Interfaces;
using api.Models;

namespace api.Services
{
    public class UserServices : IUserService
    {

        private readonly IBaseRepo<User> _repo;

        public UserServices(IBaseRepo<User> repo)
        {
            _repo = repo;
        }

        public Task<bool> IsExistsUser(int userId)
        {
           return  _repo.IsExistsELementAsync(userId);
        }
       
    }
}
