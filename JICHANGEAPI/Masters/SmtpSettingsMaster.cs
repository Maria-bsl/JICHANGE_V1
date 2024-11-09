using JichangeApi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JichangeApi.Masters
{
    public class SmtpSettingsMaster
    {
        public SmtpSetting GetBaseSupportSmtpSetting()
        {
            using (JICHANGEEntities1 entities = new JICHANGEEntities1())
            {
                var results = entities.smtp_settings.OrderByDescending(e => e.effective_date)
                    .Take(1)
                    .FirstOrDefault();
                if (results == null) return null;
                else return SmtpSetting.Create(results);
            }
        }
    }
}
