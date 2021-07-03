using CinemaShop.Domain.DomainModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaShop.Services.Interface
{
    
        public interface IEmailService
        {
            Task SendEmailAsync(List<EmailMessage> allMails);
        }

       
    
}
