using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serveris.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Role { get; set; } // 1 - Admin, 2 - normal user
        public double AccountBalance { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public string Token { get; set; }
    }
}
