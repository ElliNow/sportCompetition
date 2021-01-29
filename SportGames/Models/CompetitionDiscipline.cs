using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGames.Models
{
    //Соревновательная дисциплина
    public class CompetitionDiscipline
    {
        public int Id { get; set; }
        public int CompetitionId { get; set; }
        public virtual Competition Competition { get; set; }
        public int DisciplineId { get; set; }
        public virtual Discipline Discipline { get; set; }
        public virtual ICollection<CompetitorDiscipline> CompetitorDisciplines { get; set; }

        public override string ToString()
        {
            return Discipline.Name;
        }
    }
}
