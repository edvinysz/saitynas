using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serveris.Models
{
    public class UserItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public double AccountBalance { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
    }
}
