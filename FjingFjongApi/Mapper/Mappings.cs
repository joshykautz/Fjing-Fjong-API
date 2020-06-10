using AutoMapper;
using FjingFjongApi.Models;

namespace FjingFjongApi.Mapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Player, PlayerDto>().ReverseMap();
            CreateMap<Player, PlayerCreateDto>().ReverseMap();
            CreateMap<Player, PlayerUpdateDto>().ReverseMap();
            CreateMap<Match, MatchDto>().ReverseMap();
            CreateMap<Match, MatchCreateDto>().ReverseMap();
            CreateMap<Match, MatchUpdateDto>().ReverseMap();
        }
    }
}
