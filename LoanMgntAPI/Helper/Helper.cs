using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoanMgntAPI.Helper
{
    public static class Helper
    {
        private static IConfiguration Configuration { get; set; }


        public static JwtSecurityToken Token(string issuer, string audience, string signinKey, string email, string custId, int roleId)
        {
            var claims = new[]
                    {                        
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("user_id", custId),
                        new Claim("role_id", roleId.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, email)
                    };

            var token = new JwtSecurityToken
            (
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(60),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signinKey)),
                     SecurityAlgorithms.HmacSha256)
            );

            return token;
        }


        #region Encryption

        public static string Encrypt(string StringToEncode)
        {
            try
            {
                StringToEncode = StringToEncode.ToUpper();
                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(StringToEncode);
                string str = Convert.ToBase64String(data);
                str = str + "@";
                return str;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        #endregion

        #region Decryption

        public static string Decrypt(string stringToDecode)
        {
            string str = string.Empty;
            try
            {
                stringToDecode = stringToDecode.Replace("@", "");
                byte[] data = Convert.FromBase64String(stringToDecode);
                str = System.Text.ASCIIEncoding.ASCII.GetString(data);
                return str;
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion


        #region CreateRandomPasssword
        public static string CreateRandomPassword(int length = 8)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            return Encrypt(new string(chars));
        }
        #endregion

        public static int ConvertDateToInt(DateTime date)
        {
          return (date.Year * 10000) + (date.Month * 100) + (date.Day);
        }
    }
}
