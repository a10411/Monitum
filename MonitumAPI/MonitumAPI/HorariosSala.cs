namespace MonitumAPI
{
    public class HorariosSala
    {
        public int IdHorario { get; set; }
        public int IdSala { get; set; }
        public string DiaSemana { get; set; } = string.Empty;

        public DateTime HoraEntrada { get; set; }
        public DateTime HoraSaida { get; set; }

        public virtual Sala IdSalaNavigation { get; set; }

    }
}
