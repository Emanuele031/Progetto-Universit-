

using System.Collections.Generic;
using System.Linq;
using Models;
using Interfaces;

namespace Repositories
{
    public class StudenteRepository : IStudenteRepository
    {
        private List<Studente> studenti = new List<Studente>();

        public void Aggiungi(Studente s) => studenti.Add(s);

        public Studente CercaPerMatricola(string matricola) => studenti.FirstOrDefault(s => s.Matricola == matricola);

        public List<Studente> Tutti() => studenti;
    }
}

