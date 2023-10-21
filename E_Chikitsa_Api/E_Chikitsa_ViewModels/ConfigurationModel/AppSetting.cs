using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Chikitsa_ViewModels.ConfigurationModel
{
    public class AppSetting
    {
        public string SMTPHost { get; set; }
        public string SMTPPort { get; set; }
        public string SMTPFrom { get; set; }
        public string SMTPUsername { get; set; }
        public string SMTPPassword { get; set; }
        public string SMTPDomain { get; set;}
        public string LocalAPIURL { get; set; }

    }

    public class CacheTimeOut
    {
        public int Timeout { get; set; }
    }
}
