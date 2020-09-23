using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using smsServ.SmsUtils.Utils;

namespace smsServ.SmsUtils.DBControl
{
    public class DBConnection
    {
        OracleConnection conn;
        private string TAG = "DBConnection";
        
        public DBConnection(string ip1, string ip2, string schema)
        {
            //string ip1 = config["dbIp1"];
            //string ip2 = config["dbIp2"];
            //string schema = config["dbInstance"];
            string connStr = string.Format(@"Data Source=(DESCRIPTION=(LOAD_BALANCE=on)(FAILOVER=off)(ADDRESS_LIST=(SOURCE_ROUTE=yes)(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT=1521))(ADDRESS=(PROTOCOL=TCP)(HOST={1})(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME={2}))); User Id=uni_dish;Password=Qk6cGhLSWmpL;", ip1, ip2, schema);
            conn = new OracleConnection(connStr);
        }
        public bool iOpen()
        {
            bool retVal = false;
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                    retVal = true;
                }
                else
                {
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                LogWriter._error(TAG, ex.ToString());
            }
            return retVal;
        }
        public bool iClose()
        {
            bool retVal = false;
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    retVal = true;
                }
                else
                {
                    conn.Close();
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                retVal = false;
                LogWriter._error(TAG, ex.ToString());
            }
            return retVal;
        }
        public bool idbStatOK()
        {
            bool res = false;
            if (iOpen())
            {
                if (iClose())
                {
                    res = true;
                }
            }
            return res;
        }
        public bool idbCommand(string qry)
        {
            bool res = false;
            try
            {
                iOpen();
                OracleCommand cmd = new OracleCommand(qry, conn);
                int stt = cmd.ExecuteNonQuery();
                iClose();
                res = true;
            }
            catch (Exception ex)
            {
                LogWriter._error(TAG, ex.ToString());
                res = false;
            }
            return res;
        }
        public DataTable getTable(string qry)
        {
            DataTable dt = new DataTable();
            try
            {
                OracleDataAdapter da = new OracleDataAdapter(qry, conn);
                da.Fill(dt);
            }
            catch(Exception ex)
            {
                LogWriter._error(TAG, ex.ToString());
                dt.Clear();
            }
            return dt;
        }
    }
}
