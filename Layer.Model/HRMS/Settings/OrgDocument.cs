//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Layer.Model.HRMS.Settings
{
    using QueryGenerator;
    using System;
    using System.Collections.Generic;

    [Table("OrgDocument", "Settings")]
    public partial class OrgDocument
    {
        [PKey]
        public int SL { get; set; }
        public Nullable<int> OrgId { get; set; }
        public int DocType { get; set; }
        public string DocName { get; set; }
        public string DocURL { get; set; }
        public string FileName { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
