using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CinemaShop.Services.Interface
{
    public interface IBackgroundEmailSender
    {
        Task DoWork();
    }
}
