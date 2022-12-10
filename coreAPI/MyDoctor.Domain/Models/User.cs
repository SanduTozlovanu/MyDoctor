namespace MyDoctorApp.Domain.Models
{
    public class User
    {
        public User(string accountType, string email, string password, string firstName, string lastName)
        {
            Id = Guid.NewGuid();
            AccountType = accountType;
            Email = email;
            Password = password;
            FirstName = firstName;
            Password = password;
            LastName = lastName;
        }

        private const string SEPARATOR = ", ";
        public Guid Id { get; private set; }
        public string AccountType { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName
        {
            get { return $"{FirstName}{SEPARATOR}{LastName}"; }
        }

        public void Update(User user)
        {
            Email = user.Email;
            Password = user.Password;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
    }
}