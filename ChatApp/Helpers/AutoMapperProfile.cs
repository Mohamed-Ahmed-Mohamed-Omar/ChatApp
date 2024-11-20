using AutoMapper;
using ChatApp.Data.Entities;
using ChatApp.Models;

namespace ChatApp.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Message,CreateMessage>().ReverseMap();

            CreateMap<Message, GetAllMessages>().ReverseMap();
        }
    }
}
