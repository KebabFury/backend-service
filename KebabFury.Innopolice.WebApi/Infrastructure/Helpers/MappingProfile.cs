using AutoMapper;
using KebabFury.Innopolice.WebApi.Application.Dto;
using KebabFury.Innopolice.WebApi.Domain.Models;

namespace KebabFury.Innopolice.WebApi.Infrastructure.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // mappings here
        CreateMap<BotCommand, BotCommandDto>().ReverseMap();
    }
}