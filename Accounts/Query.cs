namespace Accounts
{
    public class Query
    {
        public IEnumerable<User> GetUsers([Service] UserRepository repository)
        {
            return repository.GetUsers();
        }

        public User GetUser(int id, [Service] UserRepository repository)
        {
            return repository.GetUser(id);
        }

        //schema {
        //  query: Query
        //    }

        //    type Query
        //    {
        //        users: [User!]!
        //  user(id: Int!): User!
        //}

        //    type User
        //    {
        //        id: Int!
        //  name: String!
        //  birthdate: DateTime!
        //  username: String!
        //}
}
}
