using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Chikitsa_ViewModels.CommoModel
{
    public class EmailModel
    {
        private string _toEmail;
        private string _ccEmail;
        private string _bccEmail;
        public string From { get; set; }

        public string To
        {
            get
            {
                return _toEmail;
            }
            set
            {
                _toEmail = value.Replace(",", ",");

            }
        }

        public string CC
        {

            get
            {
                return _ccEmail;
            }
            set
            {
                _ccEmail = value.Replace(",", ",");
            }

        }

        public string BCC
        {
            get
            {
                return _bccEmail;
            }
            set
            {
                _bccEmail = value.Replace(",", ",");
            }
        }

        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsAttachment { get; set; }
        public bool HighPreority { get; set; }
        public bool IsBodyHtml { get; set; }
        public string SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPFrom { get; set; }
        public string SMTPUsername { get; set; }
        public string SMTPPassword { get; set; }

    }

    public class EmailTemplateModel
    {
        public decimal EmailTemplateID { get; set; }
        public string TemplateHeader { get; set; }
        public string  EmailSubject { get; set; }
        public string TokenInfo { get; set; }
        public string TemplateString { get; set; }
        public string BCC { get; set; }
        public string CC { get; set; }
        public string ExpirePeriod { get; set; }
    }
}
