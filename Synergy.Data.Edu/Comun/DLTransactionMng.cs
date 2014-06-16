using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Synergy.Data.Edu
{
    public class DLTransactionMng : IDisposable
    {
        private DbConnection _connection = null;
        private DbTransaction _innerDbTransaction = null;
        private SqlDatabase sqlDB = null;

        internal DbConnection Connection
        {
            get { return _connection; }
        }

        internal DbTransaction InnerDbTransaction
        {
            get { return _innerDbTransaction; }
        }

        public static DLTransactionMng Begin()
        {
            try
            {
                DLTransactionMng retval = new DLTransactionMng();
                retval.sqlDB = new SqlDatabase(CadenaConexion.Obtener());
                retval._connection = retval.sqlDB.CreateConnection();
                retval._connection.Open();
                retval._innerDbTransaction = retval._connection.BeginTransaction();
                
                return retval;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Commit()
        {
            _innerDbTransaction.Commit();
            _connection.Close();
        }

        public void RollBack()
        {
            _innerDbTransaction.Rollback();
            _connection.Close();
        }

        #region IDisposable Members

        public void Dispose()
        {
            _innerDbTransaction.Dispose();
            if (_connection.State != System.Data.ConnectionState.Closed)
                _connection.Close();
            _connection.Dispose();            
        }
        #endregion
    }
}
