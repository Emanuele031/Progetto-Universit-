namespace Models
{
    public class Voto
    {
        public string Materia { get; set; }
        public int Valore { get; set; }

        public Voto(string materia, int valore)
        {
            Materia = materia;
            Valore = valore;
        }
    }
}
