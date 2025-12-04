using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Models
{
    public class Professore : IEntita
    {
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string CodiceId { get; set; }
        public List<string> Materie { get; set; }

        
        public Professore(string nome, string cognome, string codiceId, List<string> materie)
        {
            Nome = nome;
            Cognome = cognome;
            CodiceId = codiceId;
            Materie = materie ?? new List<string>(); 
        }

        
        public void AggiungiMateria(string materia)
        {
            if (!Materie.Contains(materia))
                Materie.Add(materia);
        }

        
        public string Id => CodiceId;


        public List<string> MaterieInsegnate => Materie;

        public override string ToString()
        {
            return $"{Nome} {Cognome} (ID: {CodiceId}) - Materie: {string.Join(", ", MaterieInsegnate)}";
        }


    }
}

