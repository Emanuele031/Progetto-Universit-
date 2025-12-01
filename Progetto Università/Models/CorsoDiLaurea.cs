using System.Collections.Generic;
using System.Linq;
using Interfaces;


namespace Models
{
    public class CorsoDiLaurea : IEntita
    {
        public string Codice { get; set; }
        public string Nome { get; set; }
        public List<Professore> Professori { get; private set; }

        public CorsoDiLaurea(string codice, string nome)
        {
            Codice = codice;
            Nome = nome;
            Professori = new List<Professore>();
        }

        public string Id => Codice;

        public void AggiungiProfessore(Professore p)
        {
            if (!Professori.Any(pr => pr.CodiceId == p.CodiceId && pr.Materia == p.Materia))
                Professori.Add(p);
        }

        public List<string> Materie() => Professori.Select(p => p.Materia).Distinct().ToList();

        public override string ToString() => $"{Nome} ({Codice}) - Professori: {string.Join(", ", Professori.Select(p => p.Nome + " " + p.Cognome))}";
    }
}
