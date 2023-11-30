using AutoMapper;
using Cabs.Areas.Website.Models;
using Cabs.Areas.Website.Responses;

namespace Cabs.Areas.Website.Requests.Program
{
    public class ProgramProfile : Profile
    {
        public ProgramProfile()
        {
            CreateMap<Programs, ProgramDTO>();
            CreateMap<ProgramDTO, Programs>();
            CreateMap<Programs, ProgramUpdateDTO>();
            CreateMap<ProgramUpdateDTO, Programs>();
            CreateMap<Programs, ProgramResponse>();
            CreateMap<ProgramResponse, Programs>();
        }
    }
}
