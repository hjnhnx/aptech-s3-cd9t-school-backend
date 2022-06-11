using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CD9TSchool.Models.Dto
{
    public class UserDto
    {
        public int id { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }
        public Utils.Enum.Role role { get; set; }
    }
}