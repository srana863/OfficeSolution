//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Layer.Model.HRMS.Emp
{
    using QueryGenerator;
    using System;
    using System.Collections.Generic;

    [Table("Employee", "Emp")]
    public partial class Employee
    {
        [PKey]
        public string EmployeeCode { get; set; }
        public int InstituteId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public int JobTypeId { get; set; }
        public decimal WorkingHoursPerWeek { get; set; }
        public int SalaryTypeId { get; set; }
        public Nullable<decimal> RateOfPay { get; set; }
        public System.DateTime JobStartDate { get; set; }
        public Nullable<System.DateTime> JobEndDate { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public int AddedByUserId { get; set; }
        public System.DateTime AddedDate { get; set; }
        public Nullable<int> UpdatedByUserId { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
