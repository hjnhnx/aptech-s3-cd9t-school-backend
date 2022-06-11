using System;

namespace CD9TSchool.Models.Dto
{
    public class GroupDto
    {
        public GroupDto()
        {
        }

        public GroupDto(Group group)
        {
            groupName = group.GroupName;
            teacherId = group.TeacherId;
            slotId = group.SlotId;
            programId = group.ProgramId;
            termId = group.TermId;
            daysOfWeek = group.DaysOfWeek;
            roomNumber = group.RoomNumber;
            startDate = group.StartDate;
            teacherName = group.Teacher.FirstName + " " + group.Teacher.FirstName;
            slotName = group.Slot.Name;
            programName = group.Program.ProgramName;
            programCode = group.Program.ProgramCode;
            termNumber = group.Term.TermNumber;
            hasGeneratedTimeTable = group.HasGeneratedTimetable;
            createdAt = group.CreatedAt;
            updatedAt = group.UpdatedAt;
            deletedAt = group.DeletedAt;
            createdBy = group.CreatedBy;
            updatedBy = group.UpdatedBy;
            deletedBy = group.DeletedBy;
        }

        public string groupName { get; set; }
        public int? teacherId { get; set; }
        public int? slotId { get; set; }
        public int? programId { get; set; }
        public int? termId { get; set; }
        public string daysOfWeek { get; set; }
        public string roomNumber { get; set; }
        public DateTime startDate { get; set; }
        public string teacherName { get; set; }
        public string slotName { get; set; }
        public string programName { get; set; }
        public string programCode { get; set; }
        public int termNumber { get; set; }
        public bool hasGeneratedTimeTable { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public string createdBy { get; set; }
        public string updatedBy { get; set; }
        public string deletedBy { get; set; }
    }
}