using System.Collections.Generic;
using System.Linq;
using Models;
using Interfaces;

namespace Repositories
{
    public class ProfessoreRepository : IProfessoreRepository
    {
        private List<Professore> professori = new List<Professore>();

        public void Aggiungi(Professore p) => professori.Add(p);

        public Professore CercaPerCodice(string codiceId) => professori.FirstOrDefault(p => p.CodiceId == codiceId);

        public List<Professore> Tutti() => professori;
    }
}
