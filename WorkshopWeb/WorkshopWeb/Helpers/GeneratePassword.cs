using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopWeb.Helpers
{
    public class GeneratePassword : IGeneratePassword
    {
        // Generate a random password of a given length (optional)  
        public async Task<string> RandomPassword(int upperCase, int lowerCase, int minNumber, int maxNumber)
        {
            StringBuilder builder = new StringBuilder();
            
            await Task.Run(() =>
            {
                builder.Append(RandomString(upperCase, true));
                builder.Append(RandomNumber(minNumber, maxNumber));
                builder.Append(RandomString(lowerCase, false));
            });

            return builder.ToString();
        }

        // Generate a random number between two numbers    
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        // Generate a random string with a given size and case.   
        // If second parameter is true, the return string is lowercase 
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        
    }
}
