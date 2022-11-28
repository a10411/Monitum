using System;
using System.Collections.Generic;

#nullable disable

namespace MonitumAPI.Models
{
    public partial class EstabelecimentoGestor
    {
        public int IdEstabelecimento { get; set; }
        public int IdGestor { get; set; }

        public virtual Estabelecimento IdEstabelecimentoNavigation { get; set; }
        public virtual Gestor IdGestorNavigation { get; set; }
    }
}
