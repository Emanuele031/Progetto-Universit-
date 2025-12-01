using Interfaces;

namespace Models
{
    public class Professore : IEntita
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string CodiceId { get; set; }
        public string Materia { get; set; }

        public Professore(string nome, string cognome, string codiceId, string materia)
        {
            Nome = nome;
            Cognome = cognome;
            CodiceId = codiceId;
            Materia = materia;
        }

        public string Id => CodiceId;
        public override string ToString() => $"{Nome} {Cognome} (ID: {CodiceId}) - Materia: {Materia}";
    }
}
