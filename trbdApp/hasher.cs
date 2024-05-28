using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace trbdApp
{
    public class hasher
    {
        public static string Hashpasswords(string pass)
        {
            using (SHA256 sha256hash = SHA256.Create())
            {
                byte[] SoursebytePass = Encoding.UTF8.GetBytes(pass);
                byte[] HashSourceBytePass = sha256hash.ComputeHash(SoursebytePass);
                string hashpass = BitConverter.ToString(HashSourceBytePass).Replace("-", string.Empty);
                return hashpass;
            }
        }
    }
}
