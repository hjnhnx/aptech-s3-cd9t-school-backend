using CD9TSchool.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CD9TSchool.Data
{
    public class SchoolManager : DbContext
    {
        public SchoolManager() : base("SchoolManager") { }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<TermSubject> TermSubjects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
    }
}