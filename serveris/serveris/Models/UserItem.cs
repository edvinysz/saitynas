using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serveris.Models
{
    public class UserItem
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public int Age { get; set; } // 0 = 0-18; 1 = 18-30; 2 = 30+
        public bool Visibility { get; set; }
        public double AccountBalance { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
    }
}
