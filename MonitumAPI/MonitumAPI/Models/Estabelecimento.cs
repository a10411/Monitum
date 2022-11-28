using System;
using System.Collections.Generic;

#nullable disable

namespace MonitumAPI.Models
{
    public partial class Estabelecimento
    {
        public Estabelecimento()
        {
            EstabelecimentoGestors = new HashSet<EstabelecimentoGestor>();
            Salas = new HashSet<Sala>();
        }

        public int IdEstabelecimento { get; set; }
        public string Nome { get; set; }
        public string Morada { get; set; }

        public virtual ICollection<EstabelecimentoGestor> EstabelecimentoGestors { get; set; }
        public virtual ICollection<Sala> Salas { get; set; }
    }
}
