namespace MonitumBOL.Models
{
    public class Gestor
    {
        public int IdGestor { get; set; }
        public string Email { get; set; }
        public string Password_Hash { get; set; }
        public string Password_Salt { get; set; }
    }
}