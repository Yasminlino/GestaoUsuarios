using AutoMapper;
using GestaoDeUsuario.Domain.Entities;
using GestaoDeUsuario.Application.DTO;

namespace GestaoDeUsuario.Application.Mapping;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Usuario, UsuarioDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Login, opt => opt.MapFrom(src => src.UserName))  // Mapeia UserName para Login
            .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.PasswordHash));  // Mapeia PasswordHash para Senha
    }
}
