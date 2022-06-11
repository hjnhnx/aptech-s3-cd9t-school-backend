using AutoMapper;
using CD9TSchool.Models;
using CD9TSchool.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CD9TSchool.Mapper
{
    public class AttendanceRecordMapper : Profile
    {
        public AttendanceRecordMapper()
        {
            CreateMap<AttendanceRecord, AttendanceRecordDto>().ReverseMap();
        }
    }
}