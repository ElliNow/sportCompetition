using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGames.Models
{
    public class Housing
    {
        public int Id { get; set; }
        [Index("name_is_unique", IsUnique = true)]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal CostPerDay { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
