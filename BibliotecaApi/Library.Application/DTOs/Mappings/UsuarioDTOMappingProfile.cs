using AutoMapper;
using BibliotecaApi.Library.Core.Model;

namespace BibliotecaApi.Library.Application.DTOs.Mappings
{
    public class UsuarioDTOMappingProfile : Profile
    { 
        public UsuarioDTOMappingProfile()
        { 
            CreateMap<UsuarioModel, UsuarioModelDTO>().ReverseMap(); 
        }
    }
}
