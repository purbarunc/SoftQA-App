using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace Softvision.QA.App.Models
{
    public class Dao
    {
        private static volatile Dao _instance;
        private static readonly object SyncRoot = new Object();

        private readonly Database _db;

        public static Dao Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new Dao();
                    }
                }
                return _instance;
            }
        }

        private Dao()
        {
            // Configure the DatabaseFactory to read its configuration from the .config file
            var factory = new DatabaseProviderFactory();
            _db = factory.Create("QaDbContext");
        }

        internal DataSet GetEmployee(string login)
        {
            DataSet ds = null;
            using (DbCommand objcmd = _db.GetStoredProcCommand("USP_Profile_GetEmployeeByLogin"))
            {
                try
                {
                    _db.AddInParameter(objcmd, "@login", DbType.String, login);
                    ds = _db.ExecuteDataSet(objcmd);
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "Data Access Policy"))
                        throw;
                }
            }
            return ds;
        }

        internal DataSet GetEmployee(int EmployeeId)
        {
            DataSet ds = null;
            using (DbCommand objcmd = _db.GetStoredProcCommand("USP_Profile_GetEmployeeByEmployeeId"))
            {
                try
                {
                    _db.AddInParameter(objcmd, "@EmployeeId", DbType.String, EmployeeId);
                    ds = _db.ExecuteDataSet(objcmd);
                }
                catch (Exception ex)
                {
                    if (ExceptionPolicy.HandleException(ex, "Data Access Policy"))
                        throw;
                }
            }
            return ds;
        }
    }
}