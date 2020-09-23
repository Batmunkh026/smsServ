using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using smsServ.SmsUtils.DBControl;
using smsServ.SmsUtils.Utils;

namespace smsServ.SmsSender
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private string TAG = "Worker";
        IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true).Build();
        DBConnection dbconn;
        private bool isBusy = false;
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            dbconn = new DBConnection(config["dbIp1"], config["dbIp2"], config["dbInstance"]);

        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                
                if (!isBusy)
                {
                    //_logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    sender();
                }
                await Task.Delay(2000, stoppingToken);
            }
        }
        private void sender()
        {
            if (dbconn.idbStatOK())
            {
                isBusy = true;
                smsWorkerBC();
                smsWorkerCare();
                isBusy = false;
            }
        }
        private void smsWorkerBC()
        {
            try
            {
                DataTable dt = dbconn.getTable(smsQry.SMSBC(config["BCLimit"]));
                foreach (DataRow dr in dt.Rows)
                {
                    string phone = dr["PHONE"].ToString();
                    string smsText = dr["CONTENT"].ToString();
                    string retry = dr["RETRY"].ToString();
                    string id = dr["ID"].ToString();
                    LogWriter._sms(TAG, string.Format("Phone: [{0}], Text: [{1}], Status:[{2}], CommandResult:[{3}]", phone, smsText, true, true));
                }
            }
            catch(Exception ex)
            {
                LogWriter._error(TAG, ex.ToString());
            }
        }
        private void smsWorkerCare()
        {
            try
            {
                DataTable dt = dbconn.getTable(smsQry.SMSCARE(config["CareLimit"]));
                foreach (DataRow dr in dt.Rows)
                {
                    string phone = dr["PHONE"].ToString();
                    string smsText = dr["CONTENT"].ToString();
                    string retry = dr["RETRY"].ToString();
                    string id = dr["ID"].ToString();
                    LogWriter._sms(TAG, string.Format("Phone: [{0}], Text: [{1}], Status:[{2}], CommandResult:[{3}]", phone, smsText, true, true));
                }
            }
            catch (Exception ex)
            {
                LogWriter._error(TAG, ex.ToString());
            }
        }
    }
}
