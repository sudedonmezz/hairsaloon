using AutoMapper;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
namespace HairArt.Infrastructure.Mapper;

public class MappingProfile : Profile
{
   public MappingProfile()
        {
            // Kullanıcı giriş modeli (DTO) ile ApplicationUser arasında eşleme
            CreateMap<UserDtoForCreation, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)) // UserName'e Email'i ata
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email)) // Email'i birebir eşleştir
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber)) // Telefon numarasını eşleştir
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)) // Ad alanını eşleştir
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName)); // Soyad alanını eşleştir
        }
}
