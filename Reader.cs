namespace Assignment1
{
    public class Reader
    {
        private string name;
        private int readerId;
        private float readingSpeed;
        private List<Record> reads;

        public Reader(string name, int readerId, float readingSpeed)
        {
            this.name = name;
            this.readerId = readerId;   
            this.readingSpeed = readingSpeed;
            this.reads = new List<Record>();
 
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int GetReaderId { 
            get { return readerId; } 
            set {  readerId = value; }
        }
        public float ReadingSpeed
        {
            get { return readingSpeed; }
            set { readingSpeed = value; }
        }
        public int GetTotalReadingTime()
        {
            return this.reads.Sum(r => r.OverallReadingTime(this.readingSpeed, false));
        }


        public void AddReading(Record record)
        {
            Reader recordReader = record.GetReader;
            if(recordReader == this)
            {
                this.reads.Add(record);
            }
            else
            {
                throw new ArgumentException("Wrong ReaderId");
            }

        }
        public float ReturnBooks(DateTime date)
        {
            float totalFee = 0.0f;
            foreach (Record record in reads)
            {
                bool isClosed = false;
                if (!isClosed)
                {
                    isClosed = true; 
                    TimeSpan overdueDuration = date - record.Borrowed;
                    int daysOverdue = (int)overdueDuration.TotalDays;
                    if (daysOverdue > 30)
                    {
                        int monthsOverdue = daysOverdue / 30;
                        int extraDays = daysOverdue % 30;
                        totalFee += monthsOverdue * 5 + extraDays * 0.10f;
                    }
                }
                record.ReturnBook();
            }
            return totalFee;
        }
    }
}
