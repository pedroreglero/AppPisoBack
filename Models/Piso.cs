using System;
using System.Collections.Generic;

#nullable disable

namespace PisoAppBackend.Models
{
    public partial class Piso
    {
        public Piso()
        {
            IntegrantesPisos = new HashSet<IntegrantesPiso>();
            Tareas = new HashSet<Tarea>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<IntegrantesPiso> IntegrantesPisos { get; set; }
        public virtual ICollection<Tarea> Tareas { get; set; }
    }
}
