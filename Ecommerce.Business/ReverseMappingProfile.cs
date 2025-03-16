using AutoMapper;
using Ecommerce.Models.DTO;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Business
{
    public class ReverseMappingProfile : Profile
    {
        public ReverseMappingProfile()
        {
            // Add all entity mappings here
            CreateMap<Product, ProductDTO>().ReverseMap();

            // If you have other DTOs, add them here
            // CreateMap<Order, OrderDTO>().ReverseMap();
            // CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
