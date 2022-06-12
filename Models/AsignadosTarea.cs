using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace PisoAppBackend.Models
{
    public partial class AsignadosTarea
    {
        public long Id { get; set; }
        public long TaskId { get; set; }
        public int UserId { get; set; }
        public DateTime AssignedOn { get; set; }
        public int AssignedBy { get; set; }

        [JsonIgnore]
        public virtual Usuario AssignedByNavigation { get; set; }
        [JsonIgnore]
        public virtual Tarea Task { get; set; }
        public virtual Usuario User { get; set; }
    }
}
