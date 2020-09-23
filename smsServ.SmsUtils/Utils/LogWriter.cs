using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace smsServ.SmsUtils.Utils
{
    public class LogWriter
    {
        public static void _error(string TAG, string logtxt)
        {
            try
            {
                string logPath = AppDomain.CurrentDomain.BaseDirectory + string.Format(@"{0}Log{0}Error", Path.DirectorySeparatorChar);
                DirectoryInfo di = new DirectoryInfo(logPath);
                if (!di.Exists)
                {
                    Directory.CreateDirectory(logPath);
                }
                StreamWriter sw = new StreamWriter(logPath + string.Format("{0}error{1}.txt", Path.DirectorySeparatorChar, DateTime.Today.ToString(ConstantValues.DATE_FORMAT_LONG)), true);
                sw.WriteLine(string.Format(@"{0} [{1}] {2}", DateTime.Now.ToString(ConstantValues.DATE_TIME_MILISECOND), TAG, logtxt));
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public static void _sms(string TAG, string logtxt)
        {
            try
            {
                string logPath = AppDomain.CurrentDomain.BaseDirectory + string.Format(@"{0}Log{0}Sms", Path.DirectorySeparatorChar);
                DirectoryInfo di = new DirectoryInfo(logPath);
                if (!di.Exists)
                {
                    Directory.CreateDirectory(logPath);
                }
                StreamWriter sw = new StreamWriter(logPath + string.Format("{0}sms{1}.txt", Path.DirectorySeparatorChar, DateTime.Today.ToString(ConstantValues.DATE_FORMAT_LONG)), true);
                sw.WriteLine(string.Format(@"{0} [{1}] {2}", DateTime.Now.ToString(ConstantValues.DATE_TIME_MILISECOND), TAG, logtxt));
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
