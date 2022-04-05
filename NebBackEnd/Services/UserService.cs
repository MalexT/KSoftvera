using AutoMapper;
using BCryptNet = BCrypt.Net.BCrypt;
using System.Collections.Generic;
using System.Linq;
using WebApi.Authorization;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Users;

namespace WebApi.Services
{
    public class UserService : IUserService
    {
        private DataContext _context;
        private IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly Logger _log;

        public UserService(
            DataContext context,
            IJwtUtils jwtUtils,
            IMapper mapper,
            Logger log)
        {
            _log = log;
            _context = context;
            _jwtUtils = jwtUtils;
            _mapper = mapper;

        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
            {
                _log.LogError("Username or password is incorrect");
                throw new AppException("Username or password is incorrect");
            }

            // authentication successful
            var response = _mapper.Map<AuthenticateResponse>(user);
            response.Token = _jwtUtils.GenerateToken(user);
            return response;
        }

        public User GetById(int id)
        {
            return getUser(id);
        }

        public void Register(RegisterRequest model)
        {
            // validate
            if (_context.Users.Any(x => x.Username == model.Username))
            {
                _log.LogError("Username: '" + model.Username + "' is already taken");
                throw new AppException("Username '" + model.Username + "' is already taken");
            }

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // hash password
            user.PasswordHash = BCryptNet.HashPassword(model.Password);

            // save user
            _context.Users.Add(user);
        }
        // helper method

        private User getUser(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                _log.LogError("User not found");
                throw new KeyNotFoundException("User not found");
            }
            return user;
        }
    }
}