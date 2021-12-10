using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace lab
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Task 1");
            bool flag = true;
            Account bankAmount = new Account();
            Console.WriteLine("Создался новый счет!");
            Console.Write("Введите сберегательный или текущий счет вы хотите установить : ");
            bankAmount.SetTypeBank(Console.ReadLine());
            Console.WriteLine();
            bankAmount.CheckType();
            Console.WriteLine(string.Format("Ваш уникальный номер {0}", bankAmount.id));
            Dictionary<SHA256, Account> hashList = new Dictionary<SHA256, Account>();
            hashList.Add(bankAmount.HashCode, bankAmount);
            Console.WriteLine();
            while (flag)
            {
                Console.WriteLine("Доступные действия : <перевести> перевести на другой счет; <снять> <положить>(со счета/на счет); <поменять> (сберегательный/текущий) ; <баланс> проверить баланс; <создать> или <удалить аккаунт> аккаунт <выйти> c программы");
                string command = Console.ReadLine().ToLower();
                Console.Clear();

                switch (command)
                {
                    case "снять":
                        Console.WriteLine("Сколько денег хотите снять?");
                        decimal money2;
                        bool flag3 = !decimal.TryParse(Console.ReadLine(), out money2);
                        if (flag3)
                        {
                            Console.WriteLine("Неверный ввод!");
                        }
                        bankAmount.Withdraw(money2);
                        break;
                    case "положить":
                        Console.WriteLine("Сколько денег хотите положить?");
                        decimal money;
                        bool flag2 = !decimal.TryParse(Console.ReadLine(), out money);
                        if (flag2)
                        {
                            Console.WriteLine("Неверный ввод!");
                        }
                        bankAmount.Deposit(money);
                        break;
                    case "поменять":
                        bankAmount.SwapBankTypes();
                        break;
                    case "баланс":
                        bankAmount.CheckBalance();
                        break;
                    case "выйти":
                        flag = false;
                        break;
                    case "перевести":
                        Console.WriteLine("Введите сумму перевода");
                        decimal summ;
                        while (!decimal.TryParse(Console.ReadLine(), out summ))
                        {
                            Console.WriteLine("Неверный ввод");
                        }
                        bankAmount.Transaction(summ);
                        break;
                    case "создать":
                        CreatorAccount.CreateAccount(hashList);
                        Console.WriteLine("Аккаунт создан!");
                        break;
                    case "удалить":
                        Console.WriteLine("По какому id удалить собираетесь удалить?");
                        Guid id;
                        if (Guid.TryParse(Console.ReadLine(), out id))
                        {
                            CreatorAccount.DeleteAccount(hashList, id);
                        }
                        else
                        {
                            Console.WriteLine("Неуспешное удаление!");
                        }
                        break;
                }
                Account.Dispose(bankAmount);
                Console.ReadKey();
                Console.Clear();
            }
            Console.WriteLine("Task 2");

            Console.WriteLine();
            Console.WriteLine("Введите высоту дома");
            uint height;
            while (!uint.TryParse(Console.ReadLine(), out height))
            {
                Console.WriteLine("Неверный ввод!");
            }
            Console.WriteLine("Введите этажность дома");
            uint levels;
            while (!uint.TryParse(Console.ReadLine(), out levels))
            {
                Console.WriteLine("Неверный ввод!");
            }
            Console.WriteLine("Введите количество подъездов");
            uint countEntranence;
            while (!uint.TryParse(Console.ReadLine(), out countEntranence))
            {
                Console.WriteLine("Неверный ввод!");
            }
            Console.WriteLine("Введите количество квартир");
            uint countFlats;
            while (!uint.TryParse(Console.ReadLine(), out countFlats))
            {
                Console.WriteLine("Неверный ввод!");
            }
            Console.Clear();
            Console.WriteLine("Был построен новый дом!Доступна следующая информация:");
            House house = new House(height, levels, countEntranence, countFlats);
            house.GetInfo();
            Console.WriteLine();
            Console.WriteLine("Высота этажа = " + house.GetHeightLevel().ToString());
            Console.WriteLine("Среднее кол-во квартир на подъезд = " + house.GetAvgFlatsInEntarances().ToString());
            Console.WriteLine("Среднее кол-во квартир на этаж = " + house.GetAvgFlatsOnLevel().ToString());
            Console.ReadKey();
        }


    }
}
    
