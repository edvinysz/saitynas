using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace serveris.Models
{
    public class Winner
    {
        public long Id { get; set; }
        public long GameId { get; set; }
        public string Win { get; set; }
    }
}
