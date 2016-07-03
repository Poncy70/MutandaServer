﻿using OrderEntry.Net.Models;
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
            ConnectionInfo connectionInfo = new ConnectionInfo();

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("SELECT a.servername, a.dbname, a.dbuser, a.dbpassword, a.devicemail, IsNull(a.idagente, 0) as idagente, IsNull(a.superuser, 0) as superuser ");
                sql.Append("FROM [orderEntry].[Autenticate] a ");
                sql.AppendFormat("WHERE a.devicemail = '{0}' ", credentials.UserId);

                DataTable dt = null;
                dt = db.ReadData(sql.ToString());

                if (dt.Rows.Count > 0)
                {
                    connectionInfo.ServerName = (string)dt.Rows[0]["servername"];
                    connectionInfo.DBName = (string)dt.Rows[0]["dbname"];
                    connectionInfo.DBUser = (string)dt.Rows[0]["dbuser"];
                    connectionInfo.DBPassword = (string)dt.Rows[0]["dbpassword"];
                    connectionInfo.SuperUser = (bool)dt.Rows[0]["superuser"];
                    connectionInfo.DeviceMail = (string)dt.Rows[0]["devicemail"]; 
                    connectionInfo.IdAgente = (int)dt.Rows[0]["idagente"]; 
                }

                if (connectionInfo.IdAgente == 0)
                    WriteErrorLog(connectionInfo, "ControllerStatic.GetDBSource", credentials.UserId);
            }
            catch (System.Exception ex)
            {
                WriteErrorLog(connectionInfo, "ControllerStatic.GetDBSource", ex.Message);
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