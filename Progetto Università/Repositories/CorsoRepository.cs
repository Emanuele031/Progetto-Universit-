using System.Collections.Generic;
using System.Linq;
using Models;
using Interfaces;

namespace Repositories
{
    public class CorsoRepository : ICorsoRepository
    {
        private List<CorsoDiLaurea> corsi = new List<CorsoDiLaurea>();

        public void Aggiungi(CorsoDiLaurea c) => corsi.Add(c);

        public CorsoDiLaurea CercaPerCodice(string codice) => corsi.FirstOrDefault(c => c.Codice == codice);

        public List<CorsoDiLaurea> Tutti() => corsi;
    }
}
