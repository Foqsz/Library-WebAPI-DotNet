using AutoMapper;
using BibliotecaApi.Library.Core.Model;

namespace BibliotecaApi.Library.Application.DTOs.Mappings
{
    public class UserModelDTOMappingProfile : Profile
    {
        public UserModelDTOMappingProfile()
        {
            CreateMap<UserLivroModel, UserLivroModelDTO>().ReverseMap();
        }
    }
}
