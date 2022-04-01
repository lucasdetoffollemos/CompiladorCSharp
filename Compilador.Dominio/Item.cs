using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compilador.Dominio
{
    public class Item
    {
        public string lexema;
        public string token;
        public string simbolo;

        public Item(string lexema, string token, string simbolo)
        {
            this.lexema = lexema;
            this.token = token;
            this.simbolo = simbolo;
        }
    }
}
