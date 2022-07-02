using System;
using System.Collections.Generic;

#nullable disable

namespace PisoAppBackend.Models
{
    public partial class Tarea
    {
        public Tarea()
        {
            AsignadosTareas = new HashSet<AsignadosTarea>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte Status { get; set; }
        public DateTime DueTo { get; set; }
        public bool? NotifyAssignees { get; set; }
        public DateTime? FinishedOn { get; set; }
        public int? FinishedBy { get; set; }
        public DateTime? CancelledOn { get; set; }
        public int? CancelledBy { get; set; }
        public int PisoId { get; set; }

        public virtual Usuario CancelledByNavigation { get; set; }
        public virtual Usuario CreatedByNavigation { get; set; }
        public virtual Usuario FinishedByNavigation { get; set; }
        public virtual Piso Piso { get; set; }
        public virtual ICollection<AsignadosTarea> AsignadosTareas { get; set; }
    }
}
