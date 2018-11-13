using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serveris.Models
{
    public class GameItem
    {
        public int Id { get; set; }
        public string FirstTeamId { get; set; }
        public string SecondTeamId { get; set; }
        public double Firstkof { get; set; }
        public double Secondkof { get; set; }
        public string Winner { get; set; }
        public bool IsComplete { get; set; }
    }
}
