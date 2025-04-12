using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProektTSPGlaven.Models.Database;

namespace ProektTSPGlaven.Models.Builder
{
    public class UserBuilder
    {
        private readonly User user;
        //todo add method for adding ICollection if possible to adjust the layout
        public UserBuilder() {
            user = new User();
        }

        public UserBuilder withUsername(string username)
        {
            user.username = username.ToLower();
            return this;
        }

        public UserBuilder withPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            user.hashedPassword = hashedPassword;
            return this;
        }

        public UserBuilder withEmail(string email) {
            user.email = email;
            return this; 
        }

        public UserBuilder withFirstName(string firstName)
        {
            user.firstName = firstName;
            return this;
        }

        public UserBuilder withLastName(string lastName)
        {
            user.lastName = lastName;
            return this;
        }

        public User build()
        {
            user.createdAt = DateTime.Now;
            return user;
        }

        
       
    }
}
