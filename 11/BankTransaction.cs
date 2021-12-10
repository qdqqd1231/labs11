using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab
{
    class BankTransaction
    {
        public enum TransactionType
        {
            Withdraw,
            Deposit,
            Transfer
        }
        public readonly DateTime dateTime = new DateTime();
        public readonly decimal summ;
        public readonly TransactionType transactionType;
        internal BankTransaction(decimal summ, TransactionType type)
        {
            this.summ = summ;
            dateTime = DateTime.Now;
            transactionType = type;
        }
    }
}

