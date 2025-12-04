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

        public void AggiungiProfessore(Professore p, List<string> materie)
        {
            if (!Professori.Any(pr => pr.CodiceId == p.CodiceId))
                Professori.Add(p);

            foreach (var m in materie)
                p.AggiungiMateria(m);
        }

        public List<string> Materie()
        {
            return Professori.SelectMany(p => p.MaterieInsegnate).Distinct().ToList();
        }

        public override string ToString()
        {
            return $"{Nome} ({Codice}) - Professori: {string.Join(", ", Professori.Select(p => p.Nome + " " + p.Cognome))}";
        }
    }
}
