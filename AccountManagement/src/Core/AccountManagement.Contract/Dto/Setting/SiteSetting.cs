using AccountManagement.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Contract.Dto.Setting
{
    public class SiteSetting: SiteSettingBaseViewModel
    {
        public DotinConfig DotinConfig { get; set; }
    }
}
