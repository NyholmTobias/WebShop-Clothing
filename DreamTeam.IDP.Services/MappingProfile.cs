using AutoMapper;
using DreamTeam.IDP.Shared.Models;
using DreamTeam.IDP.Shared.RequestModels;
using DreamTeam.IDP.Shared.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamTeam.IDP.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserRequest, UserResponse>().ReverseMap();
            CreateMap<ApplicationUser, UserResponse>().ReverseMap();
            CreateMap<ApplicationUser, UserRequest>().ReverseMap();
            CreateMap<UserResponse, UserRequest>().ReverseMap();
        }
    }
}
