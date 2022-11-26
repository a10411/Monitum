namespace MonitumAPI
{
    public class EstabelecimentoGestor
    {
        public int IdEstabelecimento { get; set; }
        public int IdGestor { get; set; }

        public virtual Estabelecimento IdEstabelecimentoNavigation { get; set; }
        public virtual Gestor IdFuncionarioNavigation { get; set; }

    }
}
