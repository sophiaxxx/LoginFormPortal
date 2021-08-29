using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.BizBase.ViewModels
{
    public class ResultViewModel
    {
        public bool IsSuccess { get; set; }

        public string Msg { get; set; }

        public object Data { get; set; }
    }
}
