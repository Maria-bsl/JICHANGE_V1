using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Models.Entities
{
    public class SmtpSetting
    {
        public static SmtpSetting Create(smtp_settings smtp)
        {
            SmtpSetting setting = new SmtpSetting();
            setting.sno = smtp.sno;
            setting.from_address = smtp.from_address;
            setting.smtp_port = smtp.smtp_port;
            setting.username = smtp.username;
            setting.smtp_password = smtp.smtp_password;
            setting.ssl_enable = smtp.ssl_enable;
            setting.effective_date = smtp.effective_date;
            setting.posted_by = smtp.posted_by;
            setting.posted_date = smtp.posted_date;
            setting.smtp_address = smtp.smtp_address;
            return setting;
        }

        public long sno { get; set; }
        public string from_address { get; set; }
        public string smtp_address { get; set; }
        public string smtp_port { get; set; }
        public string username { get; set; }
        public string smtp_password { get; set; }
        public string ssl_enable { get; set; }
        public Nullable<System.DateTime> effective_date { get; set; }
        public string posted_by { get; set; }
        public Nullable<System.DateTime> posted_date { get; set; }
    }
}
