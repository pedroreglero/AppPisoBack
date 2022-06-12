using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
