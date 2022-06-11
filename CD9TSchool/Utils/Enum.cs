using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CD9TSchool.Utils
{
    public class Enum
    {
        public enum Role { 
            ADMIN = 800,
            STAFF = 600, 
            TEACHER = 300, 
            STUDENT = 100 
        }
        public enum Gender { 
            MALE = 1, 
            FEMALE = 2
        }
        public enum RoomType { 
            CLASS_ROOM = 1, 
            MEETING_ROOM = 2, 
            THEORETICAL_THEATRE = 3, 
            REMOTE = 4 
        }
        public enum SessionStatus { 
            PENDING = 0, 
            TAKEN = 1, 
            MISSING = 2, 
            CANCELED = 3 
        }
        public enum CourseStatus { 
            PENDING = 0,
            FINISHED = 1, 
            CANCELED = 2
        }
         
    }
}