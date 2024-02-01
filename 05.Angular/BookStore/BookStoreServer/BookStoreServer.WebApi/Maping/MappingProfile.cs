using AutoMapper;
using BookStoreServer.WebApi.DTOs;
using BookStoreServer.WebApi.Models;

namespace BookStoreServer.WebApi.Maping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto,User>();
            CreateMap<Book,BookDto>();
        }
    }
}
