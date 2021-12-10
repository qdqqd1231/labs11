using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace classwork
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            bool flag0 = true;

            while (flag0)
            {

                Console.WriteLine("Введите кол-во билетов");
                int ticets;
                while ((!int.TryParse(Console.ReadLine(), out ticets) || ticets < 0))
                {
                    Console.WriteLine("Ошибка ввода! Введите число");
                }

                List<List<Person>> winnerList = new List<List<Person>>();
                List<Person> winners = new List<Person>();

                int count = 0;

                using (StreamReader reader = new StreamReader(@"Students.txt"))
                {

                    while (reader.ReadLine() != null)
                    {
                        count++;


                    }
                    reader.Close();

                }

                List<Person> people = new List<Person>();

                using (StreamReader reader = new StreamReader(@"Students.txt"))
                {
                    int temp = 0;
                    while (temp < count)
                    {

                        if (temp == 0)
                        {
                            string ticket = reader.ReadLine();

                        }
                        else
                        {
                            string[] strings = reader.ReadLine().ToLower().Split(' ').ToArray();

                            for (int i = 0; i < strings.Length; i++)
                            {
                                people.Add(new Person(strings[i], strings[i++], 10));
                                i++;
                            }
                        }
                        temp++;
                    }

                }

                for (int i = 0; i < winnerList.Count; i++)
                {
                    for (int r = 0; r < winnerList[i].Count; r++)
                    {
                        for (int t = 0; t < winners.Count; t++)
                        {
                            for (int p = 0; p < people.Count; p++)
                            {

                                if (people[p].name.Equals(winners[t].name))
                                {
                                    if (i > 1)
                                    {
                                        people[p].points -= 3;
                                    }

                                    else if (i > 2)
                                    {
                                        people[p].points -= 2;
                                    }

                                    else if (i > 3)
                                    {
                                        people[p].points -= 1;
                                    }
                                }
                            }
                        }
                    }
                }




                double score = 0;
                for (int i = 0; i < people.Count; i++)
                {
                    score += people[i].points;
                }

                double ver = 100 / score;

                double[] mas = new double[people.Count];

                for (int i = 0; i < people.Count; i++)
                {
                    mas[i] = people[i].points * ver;
                }

                Random random = new Random();

                int value = random.Next(0, 100);


                int flag = 0;
                while (flag < ticets)
                {

                    for (int i = 0; i < mas.Length; i++)
                    {
                        if (value >= mas[i])
                        {
                            winners.Add(people[i - 1]);
                            break;
                        }
                    }

                }

                winnerList.Add(winners);










                try
                {
                    Application excel = new Application();
                    Workbook workbook = excel.Workbooks.Open($"{Environment.CurrentDirectory}/ill.xlsx");
                    Worksheet worksheet = workbook.Worksheets[1];
                    object[,] readRange = worksheet.Range["A2", "B10"].Value2;
                    Dictionary<string, string> deseases = new Dictionary<string, string>();
                    for (int i = 1; i <= readRange.GetLength(0); i++)
                    {
                        deseases.Add(readRange[i, 1].ToString().ToLower(), readRange[i, 2].ToString());
                    }
                    workbook.Close();
                    workbook = excel.Workbooks.Open($"{Environment.CurrentDirectory}/Recover.xlsx");
                    worksheet = workbook.Worksheets[1];
                    readRange = worksheet.Range["G2", "G35"].Value2;
                    for (int i = 1; i <= readRange.Length; i++)
                    {
                        string readString = readRange[i, 1].ToString().ToLower();
                        foreach (var desease in deseases)
                        {
                            if (readString.Contains(desease.Key))
                            {
                                readRange[i, 1] = desease.Value;
                                break;
                            }
                        }
                    }
                    worksheet.Range["H2", "H35"].Value2 = readRange;
                    workbook.Save();
                    workbook.Close();
                    excel.Quit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                Console.ReadKey();
            }
        }
    }
}








        
    

    

