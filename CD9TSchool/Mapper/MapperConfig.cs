using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CD9TSchool.Mapper
{
    public static class MapperConfig
    {
        public static IMapper _mapper { get; set; }
        public static void Register()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new GroupMapper());
            });

            _mapper = mapperConfig.CreateMapper();
        }
        
    }
}