using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGames.Models
{
    //дисциплина
    public class Discipline
    {
        public int Id { get; set; }
        //[Index("Name", IsUnique = true)]
        public string Name { get; set; }
        public string Description { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
