using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsnEntityFrameworkCoreAppKursova.Models
{
    public interface InterfaceCl
    {
        public int Id { get; set; }
        public virtual string ToString()
        {
            return "";
        }
    }
}
