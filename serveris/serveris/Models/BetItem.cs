using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serveris.Models
{
    public class BetItem
    {
        public long Id { get; set; }
        public int GameId { get; set; }
        public int PersonId { get; set; }
        public int ChosenId { get; set; }
        public double BetMoney { get; set; }
        public double PossibleWinMoney { get; set; }
        public bool HasWon { get; set; }
    }
}
