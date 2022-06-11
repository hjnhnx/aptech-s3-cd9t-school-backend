using System;

namespace CD9TSchool.Models.Dto
{
    public class SlotDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string groupNameSuffix { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
        public string deletedBy { get; set; }
    }
}