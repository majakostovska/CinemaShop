using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaShopAdminApplication.Models
{
    public class CinemaShopUser
    {
        public string Email { get; set; }
        public string NormalizedUserName { get; set; }
        public string UserName { get; set; }
    }
}
