using System;

namespace PlaceAgregator.Entities
{
    public class Account : Entity
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public Role Role { get; private set; }

        public User? User { get; private set; }
        public Manager? Manager { get; private set; }
        public Admin? Admin { get; private set; }

        private Account() { }
        public Account(string login, string passwordHash, Role role)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentNullException(nameof(Account), $"{nameof(login)} == null");
            if (string.IsNullOrEmpty(passwordHash))
                throw new ArgumentNullException(nameof(Account), $"{nameof(passwordHash)} == null");

            this.Login = login;
            this.PasswordHash = passwordHash;
            this.Role = role;
        }
    }
}
