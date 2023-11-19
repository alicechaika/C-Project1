using System;

namespace Assignment1
{
    public class Author
    {
        private string firstName;
        private string lastName;

        public Author(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }
        public string getFirstName()
        {
            return firstName;
        }
        public string getLastName()
        {
            return lastName;
        }
        public void SetAuthorName(string firstName, string lastName)
        {
            if (IsValid(firstName) && IsValid(lastName))
            {
                this.firstName = firstName;
                this.lastName = lastName;
            }
            else
            {
                throw new ArgumentException("First name and last name must begin with a capital letter");
            }
        }

        public string GetAuthorName()
        {
            return $"{firstName} {lastName}";
        }

        public static bool IsValid(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return false;
            if (char.IsUpper(name[0]) && name.Substring(1).All(char.IsLower))
            {
                return true;
            }
            return false;
        }
    }
}
