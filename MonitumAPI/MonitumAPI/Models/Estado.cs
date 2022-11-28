using System;
using System.Collections.Generic;

#nullable disable

namespace MonitumAPI.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Salas = new HashSet<Sala>();
        }

        public int IdEstado { get; set; }
        public string Estado1 { get; set; }

        public virtual ICollection<Sala> Salas { get; set; }
    }
}
