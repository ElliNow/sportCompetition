using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGames.Models
{
    //Соревнование
    public class Competition
    {
        public int Id { get; set; }
        [Index("name_is_unique", IsUnique = true)]
        [MaxLength(100)]
        public string Name { get; set; }
        //Место проведения
        public string Location { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal PrizeFund { get; set; }
        public int HousingId { get; set; }
        public virtual Housing Housing { get; set; }
        public int TransportId { get; set; }
        public virtual Transport Transport { get; set; }
        public string Description { get; set; }
        public int DietId { get; set; }
        public virtual Diet Diet { get; set; }

        public int AmountDays { get; set; }
        
        public virtual ICollection<Competitor> Competitors { get; set; }
        public virtual ICollection<RefereeCompetition> RefereeCompetitions { get; set; }
        public virtual ICollection<CompetitionDiscipline> CompetitionDisciplines { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
