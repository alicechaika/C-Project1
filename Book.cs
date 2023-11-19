namespace Assignment1
{
    public class Book
    {
        private string title;
        private int length;
        private Author author;

        public Book(string title, int length, string authorName)
        {
            this.title = title;
            this.length = length;
            this.author = new Author("", "");
            string[] strings = authorName.Split(' ');
            this.author.SetAuthorName(strings[0], strings[1]);

        }
        public string Title { 
            get { return title; } 
            set { title = value; }
        }
        public string GetAuthor() 
        { 
            return this.author.GetAuthorName();
        }
        private Author Author { get { return this.author; } set { author = value; } }
       
        public int GetReadingTime(float minutesPerPage, bool inHours)
        {
            double totalMins = minutesPerPage * length;
            if (inHours)
            {
                return (int)Math.Ceiling(totalMins / 60);
            }
            else
            {
                return (int)Math.Ceiling(totalMins);
            }
        }
    }
}
