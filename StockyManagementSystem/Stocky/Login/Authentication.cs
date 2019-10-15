using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Stocky.Data;
using System.Configuration;
using Stocky;
using Stocky.DataAccesse;

namespace Stocky.Login
{
    public class Authentication
    {
        public const int SaltByteSize = 24;
        public const int HashByteSize = 20; // to match the size of the PBKDF2-HMAC-SHA-1 hash
        public const int Pbkdf2Iterations = 1000;
        public const int IterationIndex = 0;
        public const int SaltIndex = 1;
        public const int Pbkdf2Index = 2;

        private static string CurrentPassword;
        private static string PassWordToHash;

        

        public static string HashPassword(string password)
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SaltByteSize];
            cryptoProvider.GetBytes(salt);

            var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);
            PassWordToHash = Pbkdf2Iterations + ":" +
                   Convert.ToBase64String(salt) + ":" +
                   Convert.ToBase64String(hash);

            
            return PassWordToHash;
        }

        static bool ValidatePassword(string password)
        {
            StockyDataDataContext DB = new StockyDataDataContext();
            
            char[] delimiter = { ':' };
            var split = CurrentPassword.Split(delimiter);
            var iterations = Int32.Parse(split[IterationIndex]);
            var salt = Convert.FromBase64String(split[SaltIndex]);
            var hash = Convert.FromBase64String(split[Pbkdf2Index]);

            var testHash = GetPbkdf2Bytes(password, salt, iterations, hash.Length);
            return SlowEquals(hash, testHash);
        }

        private static bool SlowEquals(byte[] a, byte[] b)
        {
            var diff = (uint)a.Length ^ (uint)b.Length;
            for (int i = 0; i < a.Length && i < b.Length; i++)
            {
                diff |= (uint)(a[i] ^ b[i]);
            }
            return diff == 0;
        }

        private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
        {

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
            return pbkdf2.GetBytes(outputBytes);

        }

        static bool ValidateUser(string UserName)
        {
            if(UserName != null)
            {
                StockyDataDataContext DB = new StockyDataDataContext();

                var query = from U in DB.dtUsers
                            where U.UserName == UserName
                            select U;
                if (query.FirstOrDefault().UserName != null)
                {
                    StaticDataReposityory.UserID = query.FirstOrDefault().uID;
                    CurrentPassword = query.FirstOrDefault().PassWord;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
           
        }

        public static bool LoginUser(skUser user)
        { 
            if (user.Password != "" && user.UserName != "")
            {
                if(ValidateUser(user.UserName) == true)
                {
                    if (Authentication.ValidatePassword(user.Password) == true)
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        

    }
    
}
