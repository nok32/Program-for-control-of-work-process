namespace Aplication.Model
{
    public class Person
    {
        public int PersonId { set; get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsDelited { get; set; }

        public Person(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.IsDelited = false;
        }
        public Person()
        { }

        public override string ToString()
        {
            string newToString = string.Format("--------------------{0}. {1} {2}\n\r--------------------", this.PersonId, this.FirstName, this.LastName);
            return newToString;
        }
    }
}
