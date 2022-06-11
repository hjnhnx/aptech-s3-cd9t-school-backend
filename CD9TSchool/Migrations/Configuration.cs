namespace CD9TSchool.Migrations
{
    using CD9TSchool.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CD9TSchool.Data.SchoolManager>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CD9TSchool.Data.SchoolManager context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var users = new List<User>
            {
                new User{Id = 1, Email="admin@fpt.edu.vn", Role=Utils.Enum.Role.ADMIN, CreatedAt=DateTime.Now},
                new User{Id = 2, Email="phuonghd2811@gmail.com", Role=Utils.Enum.Role.ADMIN, CreatedAt=DateTime.Now},
                new User{Id = 3, Email="taohjnh2002@gmail.com", Role=Utils.Enum.Role.ADMIN, CreatedAt=DateTime.Now},
                new User{Id = 4, Email="hjnhhinh@gmail.com", Role=Utils.Enum.Role.ADMIN, CreatedAt=DateTime.Now},
                new User{Id = 5, Email="admin5@fpt.edu.vn", Role=Utils.Enum.Role.ADMIN, CreatedAt=DateTime.Now},
                new User{Id = 6, Email="quanddth2009043@fpt.edu.vn", Role=Utils.Enum.Role.STAFF, CreatedAt=DateTime.Now},
                new User{Id = 7, Email="tiachop28111@gmail.com", Role=Utils.Enum.Role.STAFF, CreatedAt=DateTime.Now},
                new User{Id = 8, Email="hjnhhjnh1403@gmail.com", Role=Utils.Enum.Role.STAFF, CreatedAt=DateTime.Now},
                new User{Id = 9, Email="staff4@fpt.edu.vn", Role=Utils.Enum.Role.STAFF, CreatedAt=DateTime.Now},
                new User{Id = 10, Email="staff5@fpt.edu.vn", Role=Utils.Enum.Role.STAFF, CreatedAt=DateTime.Now},
                new User{Id = 11, Email="luyendh@fpt.edu.vn", Role=Utils.Enum.Role.TEACHER, CreatedAt=DateTime.Now},
                new User{Id = 12, Email="ducquan1996@gmail.com", Role=Utils.Enum.Role.TEACHER, CreatedAt=DateTime.Now},
                new User{Id = 13, Email="tiachop2811@gmail.com", Role=Utils.Enum.Role.TEACHER, CreatedAt=DateTime.Now},
                new User{Id = 14, Email="hinhhinh@gmail.com", Role=Utils.Enum.Role.TEACHER, CreatedAt=DateTime.Now},
                new User{Id = 15, Email="thongdv@fpt.edu.vn", Role=Utils.Enum.Role.TEACHER, CreatedAt=DateTime.Now},
                new User{Id = 16, Email="phuonghdth2009010@fpt.edu.vn", Role=Utils.Enum.Role.STUDENT, CreatedAt=DateTime.Now},
                new User{Id = 17, Email="nguyenhjnh2002@gmail.com", Role=Utils.Enum.Role.STUDENT, CreatedAt=DateTime.Now},
                new User{Id = 18, Email="hinhnxth2009027@fpt.edu.vn", Role=Utils.Enum.Role.STUDENT, CreatedAt=DateTime.Now},
                new User{Id = 19, Email="thuannnth2009019@fpt.edu.vn", Role=Utils.Enum.Role.STUDENT, CreatedAt=DateTime.Now},
                new User{Id = 20, Email="aibvth2012000@fpt.edu.vn", Role=Utils.Enum.Role.STUDENT, CreatedAt=DateTime.Now},
            };


           var rooms = new List<Room> 
            {
                new Room{RoomNumber="A1", Capacity=30, RoomType=Utils.Enum.RoomType.CLASS_ROOM, IsDisabled=false, CreatedAt=DateTime.Now},
                new Room{RoomNumber="B2", Capacity=20, RoomType=Utils.Enum.RoomType.CLASS_ROOM, IsDisabled=false, CreatedAt=DateTime.Now},
                new Room{RoomNumber="C3", Capacity=100, RoomType=Utils.Enum.RoomType.MEETING_ROOM, IsDisabled=false, CreatedAt=DateTime.Now},
                new Room{RoomNumber="D4", Capacity=30, RoomType=Utils.Enum.RoomType.CLASS_ROOM, IsDisabled=false, CreatedAt=DateTime.Now},
                new Room{RoomNumber="E5", Capacity=30, RoomType=Utils.Enum.RoomType.CLASS_ROOM, IsDisabled=false, CreatedAt=DateTime.Now},
            };
            

            var slots = new List<Slot>
            {
                new Slot{Id=1, Name="Morning", GroupNameSuffix="M", StartTime=480, EndTime=720, CreatedAt=DateTime.Now},
                new Slot{Id=2, Name="Afternoon", GroupNameSuffix="A", StartTime=810, EndTime=1050, CreatedAt=DateTime.Now},
                new Slot{Id=3, Name="Evening", GroupNameSuffix="E", StartTime=1050, EndTime=1290, CreatedAt=DateTime.Now}
            };

            

            var holidays = new List<Holiday>
            {
                new Holiday{Id=1, Name="Tết nguyên đán", StartDate=new DateTime(DateTime.Now.Year, 01, 31), EndDate=new DateTime(DateTime.Now.Year, 02, 06), CreatedAt=DateTime.Now},
                new Holiday{Id=2, Name="Giỗ tổ Hùng Vương", StartDate=new DateTime(DateTime.Now.Year, 04, 09), EndDate=new DateTime(DateTime.Now.Year, 04, 11), CreatedAt=DateTime.Now},
                new Holiday{Id=3, Name="Giải phóng miền nam & Quốc tế Lao động", StartDate=new DateTime(DateTime.Now.Year, 04, 30), EndDate=new DateTime(DateTime.Now.Year, 05, 03), CreatedAt=DateTime.Now},
                new Holiday{Id=4, Name="Nghỉ hè", StartDate=new DateTime(DateTime.Now.Year, 07, 01), EndDate=new DateTime(DateTime.Now.Year, 07, 07), CreatedAt=DateTime.Now},
                new Holiday{Id=5, Name="Quốc khánh", StartDate=new DateTime(DateTime.Now.Year, 09, 02), EndDate=new DateTime(DateTime.Now.Year, 09, 04), CreatedAt=DateTime.Now},   
            };



            var subjects = new List<Subject>
            {
                new Subject{SubjectCode="LBEP", SubjectName="Logic Building and Elementary Programming", SessionTotal=11, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="BNGW", SubjectName="Building Next Generation Websites", SessionTotal=11, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="DDD", SubjectName="Database Design & Development (NCC Module)", SessionTotal=5, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="DM", SubjectName="Database Management (SQL Server)", SessionTotal=11, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="BSJ", SubjectName="BootStrap and jQuery", SessionTotal=5, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="ADF1", SubjectName="Application Development Fundamentals-I", SessionTotal=10, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="ADF2", SubjectName="Application Development Fundamentals-II", SessionTotal=9, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="MLJ", SubjectName="Markup Language and JSON", SessionTotal=6, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="ISA", SubjectName="Information Systerms Analysis", SessionTotal=7, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="AP", SubjectName="Application Programming", SessionTotal=10, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="WADP", SubjectName="Web Application Development using PHP", SessionTotal=11, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="DW", SubjectName="Dynamic Websites", SessionTotal=7, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="WFP", SubjectName="Windows Forms Programming", SessionTotal=11, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="EAP", SubjectName="Enterprise Application Programming", SessionTotal=11, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="WAD", SubjectName="Web Application Development", SessionTotal=11, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="ADI", SubjectName="Analysis, Design, and Implementetion", SessionTotal=7, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="AD", SubjectName="Agile Development", SessionTotal=7, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="PIIT", SubjectName="Professional Issues in IT", SessionTotal=7, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="DAWD", SubjectName="Developing Applications for Wireless Devices", SessionTotal=5, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="NSC", SubjectName="Network Security and Cryptography", SessionTotal=9, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="WCD", SubjectName="Web Component Development", SessionTotal=11, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="IASF", SubjectName="Integrating Application using Spring Framework", SessionTotal=5, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="EAD", SubjectName="Enterprise Application Development", SessionTotal=11, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="CSW", SubjectName="Creating Services for the Web", SessionTotal=5, CreatedAt=DateTime.Now},
                new Subject{SubjectCode="CP", SubjectName="Computing Project", SessionTotal=7, CreatedAt=DateTime.Now},
            };

            

            var programs = new List<Program>
            {
                new Program{Id=1, ProgramCode="DISM", ProgramName="Diploma in Information System Management", CreatedAt=DateTime.Now},
                new Program{Id=2, ProgramCode="ADSE", ProgramName="Advanced Diploma in Software Engineering", CreatedAt=DateTime.Now}
            };

            

            var terms = new List<Term>
            {
                new Term{Id=1, TermNumber=1, ProgramId=1, CreatedAt=DateTime.Now},
                new Term{Id=2, TermNumber=2, ProgramId=1, CreatedAt=DateTime.Now},
                new Term{Id=3, TermNumber=1, ProgramId=2, CreatedAt=DateTime.Now},
                new Term{Id=4, TermNumber=2, ProgramId=2, CreatedAt=DateTime.Now},
                new Term{Id=5, TermNumber=3, ProgramId=2, CreatedAt=DateTime.Now},
                new Term{Id=6, TermNumber=4, ProgramId=2, CreatedAt=DateTime.Now},
            };

            

            var term_subjecs = new List<TermSubject>
            {
                new TermSubject{Id=1, TermId=1, SubjectCode="LBEP", SortNumber=1},
                new TermSubject{Id=2, TermId=1, SubjectCode="BNGW", SortNumber=2},
                new TermSubject{Id=3, TermId=1, SubjectCode="DDD", SortNumber=3},
                new TermSubject{Id=4, TermId=1, SubjectCode="DM", SortNumber=4},
                new TermSubject{Id=5, TermId=1, SubjectCode="BSJ", SortNumber=5},
                new TermSubject{Id=6, TermId=2, SubjectCode="ADF1", SortNumber=6},
                new TermSubject{Id=7, TermId=2, SubjectCode="ADF2", SortNumber=7},
                new TermSubject{Id=8, TermId=2, SubjectCode="MLJ", SortNumber=8},
                new TermSubject{Id=9, TermId=2, SubjectCode="ISA", SortNumber=9},
                new TermSubject{Id=10, TermId=2, SubjectCode="AP", SortNumber=10},
                new TermSubject{Id=11, TermId=2, SubjectCode="WADP", SortNumber=11},
                new TermSubject{Id=12, TermId=2, SubjectCode="DW", SortNumber=12},
                new TermSubject{Id=13, TermId=3, SubjectCode="LBEP", SortNumber=13},
                new TermSubject{Id=14, TermId=3, SubjectCode="BNGW", SortNumber=14},
                new TermSubject{Id=15, TermId=3, SubjectCode="DDD", SortNumber=15},
                new TermSubject{Id=16, TermId=3, SubjectCode="DM", SortNumber=16},
                new TermSubject{Id=17, TermId=3, SubjectCode="BSJ", SortNumber=17},
                new TermSubject{Id=18, TermId=4, SubjectCode="ADF1", SortNumber=18},
                new TermSubject{Id=19, TermId=4, SubjectCode="ADF2", SortNumber=19},
                new TermSubject{Id=20, TermId=4, SubjectCode="MLJ", SortNumber=20},
                new TermSubject{Id=21, TermId=4, SubjectCode="ISA", SortNumber=21},
                new TermSubject{Id=22, TermId=4, SubjectCode="AP", SortNumber=22},
                new TermSubject{Id=23, TermId=4, SubjectCode="WADP", SortNumber=23},
                new TermSubject{Id=24, TermId=4, SubjectCode="DW", SortNumber=24},
                new TermSubject{Id=25, TermId=5, SubjectCode="WFP", SortNumber=25},
                new TermSubject{Id=26, TermId=5, SubjectCode="WAD", SortNumber=26},
                new TermSubject{Id=27, TermId=5, SubjectCode="EAP", SortNumber=27},
                new TermSubject{Id=28, TermId=5, SubjectCode="ADI", SortNumber=28},
                new TermSubject{Id=29, TermId=5, SubjectCode="AD", SortNumber=29},
                new TermSubject{Id=30, TermId=5, SubjectCode="PIIT", SortNumber=30},
                new TermSubject{Id=31, TermId=6, SubjectCode="DAWD", SortNumber=31},
                new TermSubject{Id=32, TermId=6, SubjectCode="NSC", SortNumber=32},
                new TermSubject{Id=33, TermId=6, SubjectCode="WCD", SortNumber=33},
                new TermSubject{Id=34, TermId=6, SubjectCode="IASF", SortNumber=34},
                new TermSubject{Id=35, TermId=6, SubjectCode="EAD", SortNumber=35},
                new TermSubject{Id=36, TermId=6, SubjectCode="CSW", SortNumber=36},
                new TermSubject{Id=37, TermId=6, SubjectCode="CP", SortNumber=37},
            };

            

            var teachers = new List<Teacher>
            {
                new Teacher{Id=1, UserId=11, FirstName="Luyen", LastName="Dao", CreatedAt=DateTime.Now},
                new Teacher{Id=2, UserId=12, FirstName="Quan", LastName="Do", CreatedAt=DateTime.Now},
                new Teacher{Id=3, UserId=13, FirstName="Phuong", LastName="Hoang", CreatedAt=DateTime.Now},
                new Teacher{Id=4, UserId=14, FirstName="Hinh", LastName="Xuan", CreatedAt=DateTime.Now},
                new Teacher{Id=5, UserId=15, FirstName="Thong", LastName="Do", CreatedAt=DateTime.Now},
            };

            users.ForEach(s => context.Users.AddOrUpdate(s));
            rooms.ForEach(s => context.Rooms.AddOrUpdate(s));
            slots.ForEach(s => context.Slots.AddOrUpdate(s));
            holidays.ForEach(s => context.Holidays.AddOrUpdate(s));
            subjects.ForEach(s => context.Subjects.AddOrUpdate(s));
            programs.ForEach(s => context.Programs.AddOrUpdate(s));
            terms.ForEach(s => context.Terms.AddOrUpdate(s));
            term_subjecs.ForEach(s => context.TermSubjects.AddOrUpdate(s));
            teachers.ForEach(s => context.Teachers.AddOrUpdate(s));
            context.SaveChanges();
        }
    }
}
