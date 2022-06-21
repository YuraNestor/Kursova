using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsnEntityFrameworkCoreAppKursova.Models
{
    public class Excursion : InterfaceCl
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime DateOfExcursions { get; set; }
        public int Duration { get; set; }

        public string Destination { get; set; }

        public decimal Distance { get; set; }
        public int NumberOfTourists { get; set; }

        public decimal Price { get; set; }
        public virtual Customer ExcCustomer { get; set; } = null!;

        public int ExcCustomerId { get; set; }
        public virtual ExcursionType ExcType { get; set; } = null!;
        public int ExcTypeId { get; set; }
        public virtual List<Bus> Buses { get; set; } = new List<Bus>();

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
