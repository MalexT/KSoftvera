using WebApi.Entities;
using WebApi.Models.Users;

namespace WebApi.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        User GetById(int id);
        void Register(RegisterRequest model);
    }
}
