using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGames.Models
{
    public class DietFood
    {
        public int Id { get; set; }
        public int DietId { get; set; }
        public virtual Diet Diet { get; set; }
        public  int FoodId { get; set; }
        public virtual Food Food { get; set; }

        public override string ToString()
        {
            return Food.Name;
        }
    }
}
