using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Chikitsa_ViewModels.RequestModel
{
    public class LoginServiceModel
    {
        public int LoginUserID { get; set; }
        public bool IsActive { get; set; }
        public Guid TokenId { get; set; }
        public string SessionId { get; set; }
        public string IpAddress { get; set; }
        public string UserName { get; set; }

    }
}
