using System;
using Enum = CD9TSchool.Utils.Enum;

namespace CD9TSchool.Models.Dto
{
    public class RoomDto
    {
        public string roomNumber { get; set; }
        public int capacity { get; set; }
        public Enum.RoomType roomType { get; set; }
        public string description { get; set; }
        public bool isDisabled { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
        public string deletedBy { get; set; }
    }
}