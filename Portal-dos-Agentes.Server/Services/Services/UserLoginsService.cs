using Portal_dos_Agentes.Server.DTOs;
using Portal_dos_Agentes.Server.Models;
using Portal_dos_Agentes.Server.Repositories.Interfaces;
using Portal_dos_Agentes.Server.Services.Interfaces;

namespace Portal_dos_Agentes.Server.Services.Services
{
    public class UserLoginsService :IUserLoginsService
    {
        private readonly IUserLoginsRepository _repository;
        public UserLoginsService(IUserLoginsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserLoginsDTO>?> GetUserLogins(int userId)
        {
           var user = await _repository.GetUserLogins(userId);
           if(user is null || user.UserLogins is null)
                return null;
           user.UserLogins = user.UserLogins
                .OrderByDescending(ul => ul.LoginDate)
                .ToList();

            return user.UserLogins.Select(login => new UserLoginsDTO
            {
                Id = login.Id,
                Token = login.Token,
                ExpirationDate = login.ExpirationDate,
                IsRevoked = login.IsRevoked,
                LoginDate = login.LoginDate,
                User = user.Nome
            }).ToList();
        }

        public async Task<List<UserLoginsDTO>> GetAllUserLogin()
        {
            var users = await _repository.GetAllUserLogin();
            var userLogins = new List<UserLoginsDTO>();
            foreach(var user in users)
            {
                if(user.UserLogins is not null)
                    foreach(var login in user.UserLogins)
                    {
                        userLogins.Add(new UserLoginsDTO
                        {
                            Id = login.Id,
                            Token = login.Token,
                            ExpirationDate = login.ExpirationDate,
                            IsRevoked = login.IsRevoked,
                            LoginDate = login.LoginDate,
                            User = user.Nome
                        });
                    }
            }
            return userLogins;
        }
        public async Task<List<UserLogins>> GetAll()
        {
            return await _repository.GetAllAsync();
        }

        public async Task RemoveAll() => await _repository.RemoveAll();
    }
}
