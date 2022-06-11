using AutoMapper;
using CD9TSchool.Models;
using CD9TSchool.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CD9TSchool.Mapper
{
    public class GroupMapper : Profile
    {
        public GroupMapper()
        {
            CreateMap<Group, GroupDto>().ReverseMap();
        }
    }
}