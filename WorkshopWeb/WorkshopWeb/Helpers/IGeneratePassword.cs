using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopWeb.Helpers
{
    public interface IGeneratePassword
    {
        Task<string> RandomPassword(int upperCase, int lowerCase, int minNumber, int maxNumber);
    }
}
