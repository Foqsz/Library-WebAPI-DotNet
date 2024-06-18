using AutoMapper;
using BibliotecaApi.Library.Core.Model;

namespace BibliotecaApi.Library.Application.DTOs.Mappings
{
    public class UsuarioDTOMappingProfile : Profile
    { 
        public UsuarioDTOMappingProfile()
        {
            //Perfis de mapeamento
            //Mapeamento dos objetos para DTO
            CreateMap<UsuarioModel, UsuarioModelDTO>().ReverseMap(); 
        }
    }
}
