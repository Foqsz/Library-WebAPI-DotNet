using AutoMapper;
using BibliotecaApi.Library.Core.Model;

namespace BibliotecaApi.Library.Application.DTOs.Mappings
{
    public class LivroDTOMappingProfile : Profile
    {
        public LivroDTOMappingProfile()
        {
            CreateMap<LivroModel, LivroModelDTO>().ReverseMap();
        }
    }
}
