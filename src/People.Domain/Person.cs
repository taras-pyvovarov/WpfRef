namespace People.Domain
{
    public class Person
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }

        public Person(string firstname, string lastname, string phoneNumber)
        {
            Firstname = firstname;
            Lastname = lastname;
            PhoneNumber = phoneNumber;
        }
    }
}
