using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportGames.Models
{
    //Рацион
    public class Diet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public virtual ICollection<DietFood> DietFoods { get; set; }
    }
}
