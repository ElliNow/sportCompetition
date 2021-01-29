using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGames.Models
{

    public class RefereeCompetition
    {
        public int Id { get; set; }
        public  int CompetitionId { get; set; }
        public virtual Competition Competition { get; set; }
        public  int RefereeId { get; set; }
        public virtual Referee Referee { get; set; }
    }
}
