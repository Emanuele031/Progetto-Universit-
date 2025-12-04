using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;

namespace Models
{
    public class Studente : IEntita, IComparable<Studente>
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Matricola { get; set; }
        public CorsoDiLaurea CorsoIscritto { get; set; }
        public List<Voto> Voti { get; private set; }
        public List<CorsoDiLaurea> Libretto { get; private set; } = new List<CorsoDiLaurea>();

        public Studente(string nome, string cognome, string matricola, CorsoDiLaurea corso)
        {
            Nome = nome;
            Cognome = cognome;
            Matricola = matricola;
            CorsoIscritto = corso;
            Voti = new List<Voto>();
        }

        public string Id => Matricola;

        public double Media => Voti.Any() ? Voti.Average(v => v.Valore) : 0;

        public void AggiungiVoto(Voto voto)
        {
            if (!CorsoIscritto.Materie().Contains(voto.Materia))
                throw new Exception($"Materia '{voto.Materia}' non presente nel corso {CorsoIscritto.Nome}.");
            Voti.Add(voto);
        }

        public void StampaLibretto()
        {
            Console.WriteLine($"\nLibretto di {Nome} {Cognome} (Matricola {Matricola}) - Corso: {CorsoIscritto.Nome}");
            if (!Voti.Any())
            {
                Console.WriteLine("Nessun voto presente.");
                return;
            }

            foreach (var voto in Voti)
                Console.WriteLine($"{voto.Materia}: {voto.Valore}");

            Console.WriteLine($"Media: {Media:F2}");
        }

        public int CompareTo(Studente other)
        {
            int cmp = other.Media.CompareTo(this.Media);
            if (cmp != 0) return cmp;
            cmp = string.Compare(this.Cognome, other.Cognome, StringComparison.OrdinalIgnoreCase);
            if (cmp != 0) return cmp;
            return string.Compare(this.Nome, other.Nome, StringComparison.OrdinalIgnoreCase);
        }

        public void AggiungiCorsoAlLibretto(CorsoDiLaurea corso)
        {
            if (!Libretto.Contains(corso))
                Libretto.Add(corso);
        }
    }
}
