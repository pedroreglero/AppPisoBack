using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace PisoAppBackend.Models
{
    public partial class IntegrantesPiso
    {
        public int Id { get; set; }
        public int PisoId { get; set; }
        public int UserId { get; set; }
        public DateTime JoinDate { get; set; }
        public int AssignerId { get; set; }

        [JsonIgnore]
        public virtual Usuario Assigner { get; set; }
        [JsonIgnore]
        public virtual Piso Piso { get; set; }
        public virtual Usuario User { get; set; }
    }
}
