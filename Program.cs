using System;
using System.IO;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace Assignment1
{
    public class Program
    {
        // 1. load from file as a list of records (2p)
        static List<Record> LoadRecords(string path)
        {
            List<Record> records = new List<Record>();

            try
            {
                string[] names = { "Aaron", "Jake", "Newton", "Kate", "Sophie", "Mark", "Bohdan", "Aleks", "Lloyd" };
                Random random = new Random();

                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] recStr = line.Split(',');
                        string rName = names[random.Next(0, names.Length)];
                        float randomSpeed = (float)(random.NextDouble() * (4.0 - 1.0) + 1.0);

                        Book book = new Book(recStr[1], int.Parse(recStr[2]), recStr[0]);
                        Reader reader = new Reader(rName, int.Parse(recStr[3]), randomSpeed);
                        Record record = new Record(book, reader, DateTime.Parse(recStr[4]));

                        if (recStr.Length > 5 && DateTime.TryParse(recStr[5], out DateTime returnedDate))
                        {
                            record.IsReturned = true;
                            record.SetDateReturned(DateTime.Parse(recStr[5]));
                        }

                        reader.AddReading(record);
                        records.Add(record);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while loading the records from the file:");
                Console.WriteLine(ex.Message);
            }

            return records;
        }

        // 2. find title of most commonly borrowed book from records (0.5p)
        static string FindMostReadBook(string path)
        {
            Dictionary<string, int> BookCounter = new Dictionary<string, int>();
            string mostPopularTitle = null;
            try
            {
                string[] lines = File.ReadAllLines(path);
                foreach (string line in lines)
                {
                    string[] data = line.Split(',');
                    string bookName = data[1];
                    if (BookCounter.ContainsKey(bookName))
                    {
                        BookCounter[bookName]++;
                    }
                    else
                    {
                        BookCounter.Add(bookName, 1);
                    }
                }
                int MaxBookCount = BookCounter.Max(x => x.Value);
                List<string> mostReadBooks = BookCounter.Where(x => x.Value == MaxBookCount).Select(x => x.Key).ToList();
                mostPopularTitle = BookCounter.OrderByDescending(pair => pair.Value).FirstOrDefault().Key;

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while finding the most read book(s):");
                Console.WriteLine(ex.Message);
            }
            return mostPopularTitle;
        }

        // 3. find most read author (1p)
        static string FindMostReadAuthor(string path)
        {
            string mostPopularAuthor = null;
            try
            {
                List<Record> records = LoadRecords(path);

                if (records.Count == 0) return "";

                Dictionary<string, int> authorCounter = new Dictionary<string, int>();

                foreach (Record record in records)
                {
                    Book book = record.GetBook;
                    string authorName = book.GetAuthor();

                    if (!authorCounter.ContainsKey(authorName))
                    {
                        authorCounter[authorName] = 0;
                    }

                    authorCounter[authorName]++;
                }

                mostPopularAuthor = authorCounter.OrderByDescending(pair => pair.Value).FirstOrDefault().Key;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while finding the most read author:");
                Console.WriteLine(ex.Message);


            }
            return mostPopularAuthor;
        }


        // 3. find most avid reader (0.5p)
        static int FindMostAvidReader(string path)
        {
            int mostAvidReaderId = 0;
            try
            {
                List<Record> records = LoadRecords(path);
                if (records.Count == 0)
                {
                    return 0;
                }
                Dictionary<int, int> readerCounter = new Dictionary<int, int>();
                foreach (Record record in records)
                {
                    Reader reader = record.GetReader;
                    int readerId = reader.GetReaderId;
                    if (!readerCounter.ContainsKey(readerId))
                    {
                        readerCounter[readerId] = 0;
                    }

                    readerCounter[readerId]++;
                }
                mostAvidReaderId = readerCounter.OrderByDescending(pair => pair.Value).FirstOrDefault().Key;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while finding the most avid reader:");
                Console.WriteLine(ex.Message);
            }
            return mostAvidReaderId;
        }

        // 4. calculate income to a given day (1p)
        static float CalculateIncome(string path, DateTime date)
        {
            float totalIncome = 0.0f;
            try
            {
                List<Record> records = LoadRecords(path);

                if (records.Count == 0) return 0.0f;

                foreach (Record record in records)
                {
                    if (record.IsReturned)
                    {
                        DateTime returnedDate = record.GetDateReturned();
                        totalIncome += record.GetFee(returnedDate);
                    }
                    else
                    {
                        totalIncome += record.GetFee(date);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while calculating the income:");
                Console.WriteLine(ex.Message);
            }
            return totalIncome;
        }


        static void Main(string[] args)
        {
            string path = @"C:\Users\42195\Downloads\Assignment1 (1)\Assignment1\Assignment1\library_records.txt";
            List<Record> records = LoadRecords(path);
            if (records.Count == 0)
            {
                Console.WriteLine("Can not Load records from the file");
            }
            else
            {
                foreach (Record record in records)
                {
                    Book book = record.GetBook;
                    Reader reader = record.GetReader;
                    Console.WriteLine(book.GetAuthor() + "," + book.Title + "," + reader.GetReaderId + "," + record.IsReturned + ",");
                    if (record.IsReturned)
                    {
                        Console.WriteLine(record.GetFee(record.GetDateReturned()));
                    }
                }
                Console.WriteLine($"{FindMostReadBook(path)}");
                Console.WriteLine($"{FindMostReadAuthor(path)}");
                Console.WriteLine($"{FindMostAvidReader(path)}");
                Console.WriteLine($"{FindMostReadBook(path)}");
                Console.WriteLine($"{CalculateIncome(path, DateTime.Parse("2023-11-24"))}");

            }
        }
    }
}