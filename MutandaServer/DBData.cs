using AutoMapper.Mappers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace OrderEntry.Net.Service
{
    public class DBData : IDisposable
    {
        private string mProviderName;
        private string mConnectionString;
        private DbConnection mCnn;
        private DbTransaction mTransaction;
        private DbProviderFactory mProviderFactory;

        public DBData(string connectionString)
        {
            mProviderName = "System.Data.SqlClient";
            mConnectionString = connectionString;
        }

        public DBData(string serverName, string dbBaseName, string user, string password)
        {
            mProviderName = "System.Data.SqlClient";
            mConnectionString = "Server=" + serverName + ";database=" + dbBaseName + ";user id = " + user + ";password=" + password + "; Trusted_Connection=False;Encrypt=True;";
        }

        public DBData(ConnectionInfo connectionInfo)
        {
            mProviderName = "System.Data.SqlClient";
            mConnectionString = "Server=" + connectionInfo.ServerName + ";database=" + connectionInfo.DBName + ";user id = " + connectionInfo.DBUser + ";password=" + connectionInfo.DBPassword + "; Trusted_Connection=False;Encrypt=True;";
        }

        ~DBData()
        {
            Dispose();
        }

        public void Dispose()
        {
            CloseConnection();
        }

        public void CloseConnection()
        {
            if (mCnn != null)
            {
                if (mTransaction != null)
                {
                    mTransaction.Dispose();
                    mTransaction = null;
                }

                if (mCnn.State != ConnectionState.Closed)
                    mCnn.Close();
            }
        }
        public void BeginTransaction()
        {
            if (mCnn == null)
                OpenConnection();

            mTransaction = mCnn.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (mTransaction == null)
                throw new ArgumentNullException();

            mTransaction.Commit();
            mTransaction = null;
        }

        public bool TransactionOn
        {
            get { return mTransaction != null; }
        }

        public void RollbackTransaction()
        {
            if (mTransaction == null)
                throw new ArgumentNullException();

            mTransaction.Rollback();
            mTransaction = null;
        }

        public DataTable ReadData(string sqlStatement)
        {
            OpenConnection();
            DataTable dt = null;

            DbCommand cmd = mCnn.CreateCommand();
            if (mTransaction != null)
                cmd.Transaction = mTransaction;

            try
            {
                cmd.CommandText = sqlStatement;
                DbDataAdapter da = mProviderFactory.CreateDataAdapter();
                da.SelectCommand = cmd;

                dt = new DataTable();

                da.Fill(dt);
                dt.TableName = "table0";
            }
            catch (Exception e)
            {
                e.Data.Add("SQL", sqlStatement);
                throw e;
            }

            cmd.Parameters.Clear();
            CloseConnection();
            return dt;
        }

        public void Execute(string sql)
        {
            OpenConnection();

            DbCommand cmd = mCnn.CreateCommand();
            if (mTransaction != null)
                cmd.Transaction = mTransaction;

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                e.Data.Add("SQL", sql);
                throw e;
            }
        }

        public void OpenConnection()
        {
            if (mCnn == null)
            {
                mProviderFactory = DbProviderFactories.GetFactory(mProviderName);
                mCnn = mProviderFactory.CreateConnection();
                mCnn.ConnectionString = mConnectionString;
                mCnn.Open();
            }
        }
    }
}