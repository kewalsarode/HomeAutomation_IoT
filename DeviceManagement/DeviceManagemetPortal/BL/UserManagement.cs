using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using DeviceManagemetPortal.Models;

namespace DeviceManagemetPortal.BL
{
    public class UserManagement
    {
        public UserSessionModel ValidateCredential(LoginViewModel model)
        {
            UserSessionModel validUser = null;
            try
            {
                using (var ctx = new DeviceMgmtEntities())
                {
                    string passwordHash = EncryptSha256Managed(model.Password);
                    validUser = ctx.UsersAccounts.Where(ua => ua.username == model.Username && ua.passwordHash == passwordHash).Select(lvm => new UserSessionModel { UserName = lvm.username, Id = lvm.id }).FirstOrDefault<UserSessionModel>();                    
                    //validUser = ctx.UsersAccounts.Select(lvm => new UserSessionModel { UserName = lvm.username, Id = lvm.id }).FirstOrDefault<UserSessionModel>();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return validUser;
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