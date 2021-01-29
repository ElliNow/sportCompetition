using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGames.Models
{
    public class Sportsman
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public  int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }

    
}