using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsnEntityFrameworkCoreAppKursova.Models
{
    public class ExcursionType : InterfaceCl
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Bus>? Buses { get; set; } = new List<Bus>();
        public virtual List<Excursion>? Excursions { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
