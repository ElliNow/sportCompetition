using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGames.Models
{
    //дисциплина участника
    public class CompetitorDiscipline
    {
        public int Id { get; set; }
        public int CompetitorId { get; set; }
        public virtual Competitor Competitor { get; set; }
        public  int? CompetitionDisciplineId { get; set; }
        public virtual CompetitionDiscipline CompetitionDiscipline { get; set; }
        //Заниманимое место
        public int Place { get; set; }
        public int Score { get; set; }

        [NotMapped]
        public static OutputType OutputType { get; set; }
        public override string ToString()
        {
            if (OutputType == OutputType.Detailed)
                return $"{Competitor.Sportsman.Name} - {CompetitionDiscipline.Discipline.Name}";
            if (OutputType == OutputType.AddingForm)
                return $"{Competitor.Sportsman.Name} [{Competitor.Id}]";
            if (OutputType == OutputType.DetailedWithPlaces)
            {
                return $"{Competitor.Sportsman.Name} [{Competitor.Id}] ({Place} место)";
            }
            return null;
        }

    }

    public enum OutputType
    {
        AddingForm,
        Detailed,
        DetailedWithPlaces
    }
     
}
