using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsnEntityFrameworkCoreAppKursova.Models
{
    public class Bus : InterfaceCl
    {
        public int Id { get; set; }


        public string Brand { get; set; } 
        public string Model { get; set; }

        public int Capacity { get; set; }
        public int FuelConsumption { get; set; }
        public virtual List<ExcursionType> ExcursionTypes { get; set; } = new List<ExcursionType>();
        public int BDriverId { get; set; }
        public virtual Driver BDriver { get; set; } = null!;
        //public int ExcursionId { get; set; }
        public virtual List<Excursion>? Excursions { get; set; } = new List<Excursion>();
        public override string ToString()
        {
            return $"{Brand} {Model}";
        }

    }

}
