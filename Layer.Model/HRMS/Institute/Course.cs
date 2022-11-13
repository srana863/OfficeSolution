using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Institute
{
    [Table("Course", "Institute")]
    public partial class Course
    {
        [PKey]
        public int CourseId { get; set; }
        public int InstituteId { get; set; }
        public string CourseName { get; set; }
        public bool IsActive { get; set; }
        public int AddedByUserId { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> UpdatedByUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        //test
    }
}
