using System.Data;

namespace Assignment1
{
    public class Record
    {
        private Book book;
        private Reader reader;
        private DateTime borrowed;
        private bool returned;
        private DateTime dateReturned;

        public Record(Book book, Reader reader, DateTime borrowed)
        {
            this.book = book;
            this.reader = reader;
            this.borrowed = borrowed.Date;
            this.returned = false;  
        }

        public DateTime Borrowed
        {
            get { return borrowed; }
            set { borrowed = value; }
        }
       
        public Book GetBook
        {
            get { return book; }
            set { book = value; }
        }
        public Reader GetReader
        {
            get { return reader; }
            set { reader = value; }
        }
        public bool IsReturned
        {
            get { return returned; }
            set { returned = value; }
        }
        public void SetDateReturned(DateTime date)
        {
            this.dateReturned = date;
        }
        public DateTime GetDateReturned()
        {
           return this.dateReturned;
        }
        public int OverallReadingTime(float minutesPerPage, bool inHours)
        {
            return this.book.GetReadingTime(minutesPerPage, inHours);
        }
        public void ReturnBook()
        {
            this.returned = true;
        }
        public float GetFee(DateTime date)
        {
            TimeSpan overdueDuration = date.Date - borrowed.Date;
            int daysOverreaden = overdueDuration.Days;
            if(daysOverreaden <= 30) {
                return 0;
            }
            else
            {
                int monthOverreaden = daysOverreaden / 30;
                int extraDays = daysOverreaden % 30;
                double fee = monthOverreaden * 5 + extraDays * 0.1;

               return (float)fee;
            }
        }
       
    }
}
