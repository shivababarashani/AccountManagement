using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AccountManagement.Framework.Common
{
    public enum LogType
    {
        [Display(Name = "اشکال زدایی")]
        Debug = 1,

        [Display(Name = "اطلاعات")]
        Information = 2,

        [Display(Name = "خطا")]
        Error = 3,
    }
}
