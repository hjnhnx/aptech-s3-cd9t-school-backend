using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CD9TSchool.Models
{
    public class User
    {
        public int Id { get; set; }
        [Index(IsUnique = true)]
        [StringLength(450)]
        public string Email { get; set; }
        public DateTime? TokenExpireDate { get; set; }
        public string Token { get; set; }
        public string Avatar { get; set; }
        public Utils.Enum.Role Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string DeletedBy { get; set; }

    }
}