using System;

namespace Models
{
    public class RichiestaIscrizione
    {
        public Studente Studente { get; set; }
        public CorsoDiLaurea Corso { get; set; }

        public RichiestaIscrizione(Studente studente, CorsoDiLaurea corso)
        {
            Studente = studente;
            Corso = corso;
        }

        public override string ToString()
        {
            return $"{Studente.Nome} {Studente.Cognome} -> {Corso.Nome}";
        }
    }
}
