using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.Model.ViewModel
{
    public partial class UserViewModel
    {
        public string Password { get; set; }

        public string OldPassword { get; set; }

        public string ConfirmNewPassword { get; set; }

    }
}
