using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Mvc.Ui.ViewModels
{
    public class UserViewModel
    {
        /// <summary>
        /// Account
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        public string UserPassword { get; set; }
    }
}