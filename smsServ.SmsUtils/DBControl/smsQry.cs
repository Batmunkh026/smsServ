using System;
using System.Collections.Generic;
using System.Text;

namespace smsServ.SmsUtils.DBControl
{
    public class smsQry
    {
        public static string SMSBC(string limitBC)
        {
            return string.Format("SELECT ID, PHONE, CONTENT, RETRY FROM T_DISH_SENT_SMS WHERE IS_SEND = 'N' AND TYPE = '1' AND FUTURE_DATE <= SYSDATE AND ROWNUM<={0} ORDER BY ID ASC", limitBC);
        }
        public static string SMSCARE(string limitCare)
        {
            return string.Format("SELECT ID, PHONE, CONTENT, RETRY FROM T_DISH_SENT_SMS WHERE IS_SEND ='N' AND TYPE = '2' AND FUTURE_DATE <= SYSDATE AND ROWNUM <={0} ORDER BY ID ASC", limitCare);
        }
        public static string updateSMS(string retCount, string issuccess, string response, string recordID)
        {
            return string.Format("UPDATE T_DISH_SENT_SMS SET RETRY = '{0}', IS_SEND='{1}', RESPONSE='{2}', RESPONSE_DATETIME = SYSDATE WHERE ID = {3} ", retCount, issuccess, response, recordID);
        }
        public static string getBund()
        {
            return string.Format("SELECT BUNDLE_CODE, PRODUCT_NAME FROM BANK_STRATEGY_BUNDLE");
        }
    }
}
