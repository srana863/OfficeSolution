using QueryGenerator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Layer.Model.HRMS.Nothi
{
    [Table("NothiType", "Nothi")]
    public class NothiType
    {
        [PKey]
        public int NothiTypeId { get; set; }

        public int InstituteId { get; set; }

        public string NothiTypeName { get; set; }

        public string NothiTypeNameBang { get; set; }

        public bool IsActive { get; set; }

        public int AddedByUserId { get; set; }

        public DateTime AddedDate { get; set; }

        public int? UpdatedByUserId { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
