using System;
using System.Linq;

namespace JichangeApi.Services
{
    public class OTP
    {
        public static string GenerateOTP(int a)
        {
            int length = a;
            const string validChars = "1234567890";
            Random random = new Random();
            return new string(Enumerable.Repeat(validChars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }



    }
}