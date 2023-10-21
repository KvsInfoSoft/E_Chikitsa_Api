using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace E_Chikitsa_ViewModels.ResponseModel
{
    public class UserLoginDetailModel
    {
        public int VNo { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public int IsDoc { get; set; }
        public int DocId { get; set; }
        public int EnableDupPrint { get; set; }
        public int EnableShift { get; set; }
        public string? UserDept { get; set; }
        public string? UserPermission { get; set; }
        public int IsDeactive { get; set; }
        public int UsrPrdCatFlag { get; set; }
        public int UsrPrdCatId { get; set; }
        public int IsWard { get; set; }
        public int WardId { get; set; }
    }
}
