using CinemaShop.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaShop.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<CinemaShopUser> GetAll();
        CinemaShopUser Get(string id);
        void Insert(CinemaShopUser entity);
        void Update(CinemaShopUser entity);
        void Delete(CinemaShopUser entity);
    }
}
