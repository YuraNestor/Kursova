using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsnEntityFrameworkCoreAppKursova.Models
{
    public class Driver : InterfaceCl
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //public int DBusId { get; set; }
        public virtual Bus? DBus { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
