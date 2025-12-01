using System.Collections.Generic;
using System.Linq;
using Interfaces;

namespace Repositories
{
    public class RepositoryGenerico<T> where T : IEntita
    {
        private Dictionary<string, T> elementi = new Dictionary<string, T>();

        public void Aggiungi(T item) => elementi[item.Id] = item;
        public T Cerca(string id) => elementi.ContainsKey(id) ? elementi[id] : default;
        public List<T> Tutti() => elementi.Values.ToList();
        public void Rimuovi(string id) { if (elementi.ContainsKey(id)) elementi.Remove(id); }
    }
}
