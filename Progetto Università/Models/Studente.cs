using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class Studente
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Matricola { get; set; }
        public CorsoDiLaurea CorsoIscritto { get; set; }
        public List<Voto> Voti { get; private set; }

        public Studente(string nome, string cognome, string matricola, CorsoDiLaurea corso)
        {
            Nome = nome;
            Cognome = cognome;
            Matricola = matricola;
            CorsoIscritto = corso;
            Voti = new List<Voto>();
        }

        public double Media => Voti.Any() ? Voti.Average(v => v.Valore) : 0;

        public void AggiungiVoto(Voto voto)
        {
            if (!CorsoIscritto.Materie().Contains(voto.Materia))
                throw new Exception($"Materia '{voto.Materia}' non presente nel corso di laurea {CorsoIscritto.Nome}.");
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
    }
}

