﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Chikitsa_ViewModels.RequestModel
{
    public class UserLoginModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? CaptchaId { get; set; }
        public string? CaptchaText { get; set; }
    }
}
