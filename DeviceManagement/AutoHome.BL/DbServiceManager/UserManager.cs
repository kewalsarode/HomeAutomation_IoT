using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoHome.Models;
using AutoHome.Models.Repository;
using AutoHome.Models.EF;
using System.Security.Cryptography;

namespace AutoHome.BL.DbServiceManager
{
    public class UserManager
    {
        IRepository<UsersAccount> objRepository = new Repository<UsersAccount>(new DeviceManagementEntities());

        public User ValidateUser(string username, string password)
        {
            string passwordHash = EncryptSha256Managed(password);
            var user = objRepository.SearchFor(u => u.username.Equals(username) && u.passwordHash.Equals(passwordHash)).FirstOrDefault();

            if (user == null)
                return null;

            return new User()
            {
                Id = user.id,
                Username = user.username
            };
        }

        public static string EncryptSha256Managed(string input)
        {
            UnicodeEncoding uEncode = new UnicodeEncoding();
            byte[] bytClearString = Encoding.UTF8.GetBytes(input);
            SHA256Managed sha = new SHA256Managed();
            string strHex = string.Empty;
            byte[] hash = sha.ComputeHash(bytClearString);
            string hex = BitConverter.ToString(hash);
            hex = hex.Replace("-", "");
            return hex;
        }
    }
}
