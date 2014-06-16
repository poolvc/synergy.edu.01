using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace Synergy.Data.Edu
{
    public class DLTransaction
    {
        protected DLTransactionMng _Transaction;

        public DLTransactionMng Transaction
        {
            get { return _Transaction; }
            set { _Transaction = value; }
        }
    }
}
