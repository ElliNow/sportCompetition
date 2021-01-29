using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGames.Models
{
    //участник
    public class Competitor
    {
        public int Id { get; set; }
        public  int CompetitionId { get; set; }
        public virtual Competition Competition { get; set; }
        public  int SportsmanId { get; set; }
        public virtual Sportsman Sportsman { get; set; }

        public override string ToString()
        {
            return $"{Id} - {Sportsman.Name}";
        }
    }
}
