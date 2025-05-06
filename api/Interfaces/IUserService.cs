namespace api.Interfaces
{
    public interface IUserService
    {
        public Task<bool> IsExistsUser(int  userId);

    }
}
