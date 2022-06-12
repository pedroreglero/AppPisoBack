using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace PisoAppBackend.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            AsignadosTareaAssignedByNavigations = new HashSet<AsignadosTarea>();
            AsignadosTareaUsers = new HashSet<AsignadosTarea>();
            IntegrantesPisoAssigners = new HashSet<IntegrantesPiso>();
            IntegrantesPisoUsers = new HashSet<IntegrantesPiso>();
            TareaCancelledByNavigations = new HashSet<Tarea>();
            TareaCreatedByNavigations = new HashSet<Tarea>();
            TareaFinishedByNavigations = new HashSet<Tarea>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string HashedPassword { get; set; }
        public string Name { get; set; }
        public int Stars { get; set; }

        [JsonIgnore]
        public virtual ICollection<AsignadosTarea> AsignadosTareaAssignedByNavigations { get; set; }
        [JsonIgnore]
        public virtual ICollection<AsignadosTarea> AsignadosTareaUsers { get; set; }
        [JsonIgnore]
        public virtual ICollection<IntegrantesPiso> IntegrantesPisoAssigners { get; set; }
        [JsonIgnore]
        public virtual ICollection<IntegrantesPiso> IntegrantesPisoUsers { get; set; }
        [JsonIgnore]
        public virtual ICollection<Tarea> TareaCancelledByNavigations { get; set; }
        [JsonIgnore]
        public virtual ICollection<Tarea> TareaCreatedByNavigations { get; set; }
        [JsonIgnore]
        public virtual ICollection<Tarea> TareaFinishedByNavigations { get; set; }
    }
}
