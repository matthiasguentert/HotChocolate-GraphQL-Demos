namespace SchemaFirstGraphQLServer.Data
{
    public class PersonRepository
    {
        public List<Person> _persons = new List<Person>
        {
            new(1, "Amanda", 32, "FEMALE"),
            new(2, "Jon", 3, "MALE"),
            new(3, "Chloe", 13, "FEMALE"),
            new(4, "Bill", 68, "MALE")
        };

        public IEnumerable<Person> GetPersons() => _persons;
    }
}
