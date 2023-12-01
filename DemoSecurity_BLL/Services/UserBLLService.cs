using DemoSecurity_BLL.Interface;
using DemoSecurity_DAL.Entities;
using DemoSecurity_DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crypt = BCrypt.Net;

namespace DemoSecurity_BLL.Services
{
    public class UserBLLService : IUserBLLService
    {
        private readonly IUserRepository _userRepo;
        public UserBLLService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public void Register(string nickname, string password, string email)
        {
            //string salt = Crypt.BCrypt.GenerateSalt();
            //string hash = Crypt.BCrypt.HashPassword(password, salt);
            string hash = Crypt.BCrypt.HashPassword(password);

            _userRepo.Register(nickname, hash, email);
        }

        public User Login(string email, string password)
        {
            string pwdToCheck = _userRepo.CheckPassword(email);
            if (Crypt.BCrypt.Verify(password, pwdToCheck))
            {
                return _userRepo.Login(email);
            }
            throw new InvalidOperationException("Mot de passe incorrect");
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepo.GetUsers();
        }

        public void SetAdmin(int id)
        {
            if (_userRepo.CheckIsAdmin(id))
                throw new InvalidOperationException("Utilisateur déjà admin");
            _userRepo.SetAdmin(id);
        }
    }
}
