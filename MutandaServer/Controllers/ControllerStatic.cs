using OrderEntry.Net.Models;
using System.Data;
using System.Text;
using Microsoft.Azure.Mobile.Server.Authentication;

namespace OrderEntry.Net.Service
{
    public static class ControllerStatic
    {
        public static ConnectionInfo GetDBSource(ProviderCredentials credentials)
        {
            DBData db = new DBData("mxeqxlr2h5.database.windows.net,1433", "orderEntryDB", "TrilogikSa", "Password.1");

            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT a.servername, a.dbname, a.dbuser, a.dbpassword, a.devicemail, IsNull(a.idagente, 0) as idagente, IsNull(a.superuser, 0) as superuser ");
            sql.Append("FROM [orderEntry].[Autenticate] a ");
            sql.AppendFormat("WHERE a.devicemail = '{0}' ", credentials.UserId);

            DataTable dt = null;
            string deviceMail = "";
            int idAgente = 0;

            try
            {
                dt = db.ReadData(sql.ToString());

                DBData dbAgente = new DBData("mxeqxlr2h5.database.windows.net,1433", "LamandeDB", "TrilogikSa", "Password.1");
                string sqlAgente = "SELECT DeviceMail, IdAgente FROM DEVICE_ParametriDevice WHERE DeviceMail = '" + credentials.UserId + "'";
                DataTable dtAgente = dbAgente.ReadData(sqlAgente);

                if (dtAgente.Rows.Count > 0)
                {
                    deviceMail = (string)dtAgente.Rows[0]["DeviceMail"];
                    idAgente = (int)dtAgente.Rows[0]["IdAgente"];
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            
            ConnectionInfo connectionInfo = new ConnectionInfo();

            if (dt.Rows.Count > 0)
            {
                connectionInfo.ServerName = (string)dt.Rows[0]["servername"];
                connectionInfo.DBName = (string)dt.Rows[0]["dbname"];
                connectionInfo.DBUser = (string)dt.Rows[0]["dbuser"];
                connectionInfo.DBPassword = (string)dt.Rows[0]["dbpassword"];
                connectionInfo.DeviceMail = deviceMail;
                connectionInfo.IdAgente = idAgente;
                connectionInfo.SuperUser = (bool)dt.Rows[0]["superuser"];
            }

            return connectionInfo;
        }

        public static void WriteErrorLog(ConnectionInfo connectionInfo, string controller, System.Exception ex, string sqlString)
        {
            DBData db = new DBData(connectionInfo);
            StringBuilder sql = new StringBuilder();

            sql.Append("INSERT INTO ErrorLog(ErrorDate, DeviceMail, Controller, MessageText, InnerException, StackTrace, SqlString) ");
            sql.AppendFormat("VALUES(getdate(), '{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", connectionInfo.DeviceMail, controller, 
                              ex.Message.Replace("'", "''"), ex.InnerException != null ? 
                              ex.InnerException.ToString().Replace("'", "''"): "", ex.StackTrace.Replace("'", "''"), sqlString);

            db.Execute(sql.ToString());
            db.CloseConnection();
        }

        public static void WriteErrorLog(ConnectionInfo connectionInfo, string controller, string messageText)
        {
            DBData db = new DBData(connectionInfo);
            StringBuilder sql = new StringBuilder();

            sql.Append("INSERT INTO ErrorLog(ErrorDate, DeviceMail, Controller, MessageText) ");
            sql.AppendFormat("VALUES(getdate(), '{0}', '{1}', '{2}')", connectionInfo.DeviceMail, controller, messageText);

            db.Execute(sql.ToString());
            db.CloseConnection();
        }
    }
}