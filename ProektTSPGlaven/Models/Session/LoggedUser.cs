using System.Data;
using System.Runtime.CompilerServices;
using ProektTSPGlaven.Models.Database;

namespace ProektTSPGlaven.Models.Session
{
    public class LoggedUser
    {
        private User user;
        public LoggedUser(User user)
        {
            this.user = user;
        }

        public User getUser()
        {
            return user;
        }
    }
}
