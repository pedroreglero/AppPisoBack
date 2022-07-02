using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PisoAppBackend.Models
{
    public partial class Usuario
    {
        [NotMapped]
        public List<Tarea> Assigned_Tasks
        {
            get
            {
                List<Tarea> tareas = new List<Tarea>();
                foreach (AsignadosTarea at in AsignadosTareaAssignedByNavigations)
                {
                    tareas.Add(at.Task);
                }
                return tareas;
            }
        }

        [NotMapped]
        public Piso Assigned_Piso
        {
            get
            {
                if (this.IntegrantesPisoAssigners == null || this.IntegrantesPisoAssigners.Count == 0)
                    return null;
                else
                    return this.IntegrantesPisoAssigners.First().Piso;
            }
        }
    }
}
