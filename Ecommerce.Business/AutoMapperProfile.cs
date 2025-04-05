using AutoMapper;
using Ecommerce.Models.Entity;
using Ecommerce.Models.DTO;
using Ecommerce.Models.DTO.ProductDTO;
using Ecommerce.Models.DTO.Cart;
using Ecommerce.Models.DTO.Order;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // Product mappings
        CreateMap<Product, ProductResponseDto>()
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Inventory.Quantity));

        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.Inventory, opt => opt.Ignore()); // Handled manually in service

        // If needed, map back from response to entity (optional)
        CreateMap<ProductResponseDto, Product>();

        CreateMap<CartItem, CartItemDto>()
    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price));
        CreateMap<CartItemDto, CartItem>()
            .ForMember(dest => dest.Product, opt => opt.Ignore()); // Ignore to prevent circular reference
        CreateMap<CartItem, CartItemDto>();

        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        CreateMap<OrderItemDto, OrderItem>();

        CreateMap<Cart, CartDto>();
        CreateMap<CartItem, CartItemDto>();
        CreateMap<Order, OrderDto>();
        CreateMap<OrderItem, OrderItemDto>();
        CreateMap<Review, ReviewDto>().ReverseMap();

    }
}
