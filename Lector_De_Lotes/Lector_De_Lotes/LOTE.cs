using System;
using System.Collections.Generic;
using System.Text;

namespace Lector_De_Lotes
{
    class LOTE
    {
        public string NoLote { get; set; }
        public string Manzana { get; set; }
        public string Superficie { get; set; }
        public string Uso { get; set; }
        public List<COLINDANCIAS> Colindancias { get; set; }
    }
}
