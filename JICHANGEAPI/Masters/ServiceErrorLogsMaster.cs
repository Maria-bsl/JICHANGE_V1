using JichangeApi.Controllers.setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JichangeApi.Masters
{
    public class ServiceErrorLogsMaster
    {

        public service_error_logs CreateServiceErrorLog(string message)
        {
            using (JICHANGEEntities1 entities = new JICHANGEEntities1())
            {
                try
                {
                    service_error_logs errorLog = new service_error_logs()
                    {
                        error = message,
                        posted_date = DateTime.Now
                    };
                    return errorLog;
                }
                catch(Exception ex)
                {
                    return null;
                }
            }
        }

        public void AddErrorLogs(string message)
        {
            using (JICHANGEEntities1 entities = new JICHANGEEntities1())
            {
                service_error_logs errorLog = CreateServiceErrorLog(message);
                entities.service_error_logs.Add(errorLog);
                entities.SaveChanges();
            }
        }
    }
}