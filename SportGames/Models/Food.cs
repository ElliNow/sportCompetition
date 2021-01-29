using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGames.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        [Index("name_is_unique", IsUnique = true)]
        [MaxLength(100)]
        public  string Name { get; set; }
        public decimal Cost { get; set; }
        public decimal Size { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
