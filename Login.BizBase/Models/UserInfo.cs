using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.BizBase.Models
{
    public class UserInfo
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public bool IsValidate { get; set; }
    }
}
