using ApiAuthors.DTOs;
using ApiAuthors.Models;
using AutoMapper;

namespace ApiAuthors.Util;

public class AutomapperProfiles: Profile
{
    public AutomapperProfiles()
    {
        CreateMap<AuthorRequestDTO, AuthorModel>();
        CreateMap<AuthorModel, AuthorResponseDTO>();
        
        CreateMap<BookRequestDTO, BookModel>();
    }
}