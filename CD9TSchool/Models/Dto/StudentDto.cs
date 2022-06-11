using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CD9TSchool.Models.Dto
{
    public class StudentDto
    {
        public string rollNumber { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Utils.Enum.Gender gender { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public string identityNumber { get; set; }
        public DateTime? identityDateOfIssue { get; set; }
        public string identityPlaceOfIssue { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string parentFullName { get; set; }
        public string parentAddress { get; set; }
        public string parentPhoneNumber { get; set; }
        public string parentEmail { get; set; }
        public string parentOccupation { get; set; }
        public string groupName { get; set; }
        public string programName { get; set; }
     }
}