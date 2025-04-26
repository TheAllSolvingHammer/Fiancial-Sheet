using System.Data;
using System.Runtime.CompilerServices;
using ProektTSPGlaven.Models.Database;

namespace ProektTSPGlaven.Models.Session
{
    public class LoggedUser
    {
        public User user { get; set; }
        public LoggedUser(User user)
        {
            this.user = user;
        }
  
    }
}
