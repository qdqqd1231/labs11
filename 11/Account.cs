using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
namespace lab
{
    internal class Account

    {
        public enum AccountType
        {
            Save,
            Current
        }
        public void Deposit(decimal money)
        {
            bool flag = bankType == AccountType.Save;
            if (flag)
            {
                bool flag2 = money > 0;
                if (flag2)
                {
                    bankTransactions.Enqueue(new BankTransaction(money, BankTransaction.TransactionType.Deposit));
                    balanceSave += money;
                }
                else
                {
                    Console.WriteLine("Вы не можете положить отрицательное значение денег");
                }
                Console.WriteLine("Текущий баланс " + balanceSave.ToString());
            }
            else
            {
                bool flag3 = money > 0m;
                if (flag3)
                {
                    balanceCurrent += money;
                    bankTransactions.Enqueue(new BankTransaction(money, BankTransaction.TransactionType.Deposit));
                }
                else
                {
                    Console.WriteLine("Вы не можете положить отрицательное значение денег");
                }
                Console.WriteLine("Текущий баланс " + balanceCurrent.ToString());
            }
        }
        public void Withdraw(decimal money)
        {
            bool flag = bankType == AccountType.Save;
            if (flag)
            {
                bool flag2 = money <= balanceSave && money > 0;
                if (flag2)
                {
                    balanceSave -= money;
                    bankTransactions.Enqueue(new BankTransaction(money, BankTransaction.TransactionType.Withdraw));
                }
                else
                {
                    Console.WriteLine("Вы не можете снять отрицательное значение денег/недостаточный баланс");
                }
                Console.WriteLine("Текущий баланс " + balanceSave.ToString());
            }
            else
            {
                bool flag3 = money <= balanceCurrent && money > 0m;
                if (flag3)
                {
                    bankTransactions.Enqueue(new BankTransaction(money, BankTransaction.TransactionType.Withdraw));
                    balanceCurrent -= money;
                }
                else
                {
                    Console.WriteLine("Вы не можете снять отрицательное значение денег/недостаточный баланс");
                }
                Console.WriteLine("Текущий баланс " + balanceCurrent.ToString());
            }
        }
        public void SwapBankTypes()
        {
            bool flag = bankType == AccountType.Save;
            if (flag)
            {
                bankType = AccountType.Current;
            }
            else
            {
                bankType = AccountType.Save;
            }
            Console.WriteLine("Текущий счет - " + bankType.ToString());
        }

        public void SetTypeBank(string type)
        {
            bool flag = type.ToLower().Equals("сберегательный");
            if (flag)
            {
                bankType = AccountType.Save;
            }
            else
            {
                bankType = AccountType.Current;
            }
        }
        public void CheckBalance()
        {
            bool flag = bankType == AccountType.Save;
            if (flag)
            {
                Console.WriteLine("Текущий баланс " + balanceSave.ToString());
            }
            else
            {
                Console.WriteLine("Текущий баланс " + balanceCurrent.ToString());
            }
        }
        public void Transaction(decimal summ)
        {
            if (bankType == AccountType.Current)
            {
                if (balanceCurrent - summ >= 0 && summ > 0)
                {
                    balanceCurrent -= summ;
                    balanceSave += summ;
                    bankTransactions.Enqueue(new BankTransaction(summ, BankTransaction.TransactionType.Transfer));
                }
                else
                {
                    Console.WriteLine("Операция невозможна");
                }

            }
            else
            {
                if (balanceSave - summ >= 0 && summ > 0)
                {
                    balanceCurrent += summ;
                    balanceSave -= summ;
                    bankTransactions.Enqueue(new BankTransaction(summ, BankTransaction.TransactionType.Transfer));
                }
                else
                {
                    Console.WriteLine("Операция невозможна");
                }
            }
        }

        public static void Dispose(Account bankAmount)
        {
            for (int i = 0; i < bankAmount.bankTransactions.Count; i++)
            {
                string info = GetInfoAboutTransaction(bankAmount.bankTransactions.Dequeue());

                File.AppendAllText("transaction.txt", info + "\n");

            }
            GC.SuppressFinalize(bankAmount);
            Console.WriteLine("Информация о всех транзакциях записана в файл transaction.txt");
        }
        public void CheckType()
        {
            Console.WriteLine("Текущий тип банковского счета " + bankType.ToString());
        }
        public static string GetInfoAboutTransaction(BankTransaction bankTrans)
        {
            return $" Время выполнения операции {bankTrans.dateTime}; сумма перевода {bankTrans.summ} ; тип операции {bankTrans.transactionType}";
        }
        internal Account()
        {
            guid = Guid.NewGuid();
            balanceCurrent = 0;
            balanceSave = 0;
            HashCode = SHA256.Create();

        }


        public Guid id
        {
            get { return guid; }
        }
        public SHA256 HashCode { get; private set; }
        private Guid guid;
        private decimal balanceCurrent;
        private decimal balanceSave;
        private AccountType bankType;
        private Queue<BankTransaction> bankTransactions = new Queue<BankTransaction>();


        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $"{id}. Тип: {AccountType.Current}, баланс: {balanceCurrent}";
        }
        public static bool operator ==(Account bankAccount1, Account bankAccount2)
        {
            if (bankAccount1.bankType == AccountType.Current)
            {
                return bankAccount1.bankType == bankAccount2.bankType
                && bankAccount1.balanceCurrent == bankAccount2.balanceCurrent;
            }
            else
            {
                return bankAccount1.bankType == bankAccount2.bankType
                    && bankAccount1.balanceSave == bankAccount2.balanceSave;
            }
        }


        public static bool operator !=(Account bankAccount1, Account bankAccount2)
        {
            if (bankAccount1.bankType == AccountType.Current)
            {
                return !(bankAccount1.bankType == bankAccount2.bankType
                && bankAccount1.balanceCurrent == bankAccount2.balanceCurrent);
            }
            else
            {
                return !(bankAccount1.bankType == bankAccount2.bankType
                && bankAccount1.balanceSave == bankAccount2.balanceSave);
            }

        }

        public override bool Equals(object obj)
        {
            if (obj is Account)
            {
                return this == (obj as Account);
            }
            else
            {
                return false;
            }
        }
    }
}

