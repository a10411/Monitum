using System;
using System.Collections.Generic;

#nullable disable

namespace MonitumAPI.Models
{
    public partial class Gestor
    {
        public Gestor()
        {
            EstabelecimentoGestors = new HashSet<EstabelecimentoGestor>();
        }

        public int IdGestor { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<EstabelecimentoGestor> EstabelecimentoGestors { get; set; }
    }
}
