namespace GestionFormation.Entities
{
    public class SessionDeFormation
    {
        public int SessionDeFormationId { get; set; }
        public int FormateurId { get; set; }
        public Formateur Formateur { get; set; }
        public int FormationId { get; set; }
        public Formateur Formation { get; set; }
        public int SessionDeCursusId { get; set; }
        public  SessionDeCursus SessionDeCursus { get; set; }
        
    }
}