using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkshopWeb.Helpers
{
    public interface IMailHelper
    {
        void SendMail(string to, string subject, string body);

        string BodyMailConfirmation(string link, string name, string url);
    }
}
