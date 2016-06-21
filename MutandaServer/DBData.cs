using AutoMapper.Mappers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;

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
            Execute(sql, new DbParameters());
        }

        public void Execute(string sql, DbParameters parameters)
        {
            OpenConnection();

            DbCommand cmd = mCnn.CreateCommand();

            if (parameters != null)
                foreach (DbParam paramObj in parameters)
                    cmd.Parameters.Add(GetCommandParameter(cmd, paramObj));

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

        private DbParameter GetCommandParameter(DbCommand cmd, DbParam parameter)
        {
            DbParameter parameterObject = cmd.CreateParameter();
            parameterObject.ParameterName = parameter.Name;
            parameterObject.DbType = parameter.TypeDB;

            parameterObject.Value = parameter.Value;

            if (parameter.TypeDB == DbType.String)
               parameterObject.Size = Convert.ToString(parameterObject.Value).Length;

            if (parameter.TypeDB == DbType.Date)
            {
                DateTime nullValue = new DateTime(0001, 1, 1);
                parameterObject.Value = ((DateTime)parameter.Value).CompareTo(nullValue) == 0 ? DBNull.Value : parameter.Value;
            }

            if (parameter.TypeDB == DbType.Decimal)
            {
                IDbDataParameter dbParameter = parameterObject as IDbDataParameter;
                dbParameter.Precision = 19;
                dbParameter.Scale = 8;
            }

            if (parameterObject.Value == null)
                parameterObject.Value = DBNull.Value;

            return parameterObject;
        }
    }

    public class DbParam
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public DbType TypeDB { get; set; }

        public DbParam(string parameterName, DbType fieldType, object value)
        {
            Name = parameterName;
            TypeDB = fieldType;
            Value = value;
        }
    }

    public class DbParameters : IEnumerable
    {
        private List<DbParam> mParamList = new List<DbParam>();

        public DbParameters()
            : base()
        { }

        /// <exclude />
        public DbParameters(DbParam par)
            : base()
        {
            Add(par);
        }

        /// <summary>
        /// Ritorna il numero degli item presenti
        /// </summary>
        public int Count { get { return mParamList.Count; } }

        /// <summary>
        /// Permette l'accesso non tipizzato al valore del campo richiesto
        /// </summary>
        /// <param name="i">Posizione del campo</param>
        /// <returns>Valore del campo (object)</returns>  
        public DbParam this[int i]
        {
            get { return mParamList[i]; }
        }

        public DbParam this[string name]
        {
            get { return FindByName(name); }
        }

        public DbParam FindByName(string name)
        {
            var result = from DbParam c in mParamList
                         where c.Name == name
                         select c;

            if (result.Count<DbParam>() == 0)
                return null;
            else
                return (DbParam)result.First<DbParam>();
        }

        /// <summary>
        /// Aggiunge un item alla lista
        /// </summary>
        /// <param name="parameter">Istanza da aggiungere</param>
        public void Add(DbParam parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("parameter");

            if (string.IsNullOrEmpty(parameter.Name))
                throw new ArgumentException("Il parametro deve avere un nome valido");

            if (FindByName(parameter.Name) != null)
                throw new Exception("il parametro è già definito nell'elenco dei parametri: parametro " + parameter.Name);

            mParamList.Add(parameter);
        }

        public IEnumerator GetEnumerator()
        {
            return mParamList.GetEnumerator();
        }
    }
}