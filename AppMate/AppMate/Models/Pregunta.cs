using System;
using System.Collections.Generic;

namespace AppMate.Models
{
    public partial class Pregunta
    {
        public int IdPreguntas { get; set; }
        public int? IdCodigo { get; set; }
        public string Descripcion { get; set; }

        public Codigo IdCodigoNavigation { get; set; }
    }
}
