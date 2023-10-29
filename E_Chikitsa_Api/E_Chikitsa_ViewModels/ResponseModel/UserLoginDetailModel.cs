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
        public string UserName { get; set; }
        public string Password { get; set; }
        public Int16 IsDoc { get; set; }
        public int DocId { get; set; }
        public Int16 EnableDupPrint { get; set; }
        public Int16 EnableShift { get; set; }
        public string UserDept { get; set; }
        public string UserPermission { get; set; }
        public Int16 IsDeactive { get; set; }
        public Int16 UsrPrdCatFlag { get; set; }
        public Int16 UsrPrdCatId { get; set; }
        public Int16 IsWard { get; set; }
        public int WardId { get; set; }
        public Int16 IsOnline { get; set; }
    }
}
