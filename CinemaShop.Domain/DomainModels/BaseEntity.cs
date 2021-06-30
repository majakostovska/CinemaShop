using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CinemaShop.Domain.DomainModels
{
    public class BaseEntity
    {
        
        public Guid Id { get; set; }
    }
}
