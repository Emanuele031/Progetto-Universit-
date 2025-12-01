using System.Collections.Generic;

namespace Services
{
    public class StoricoOperazioni
    {
        private Stack<string> operazioni = new Stack<string>();

        public void Registra(string op) => operazioni.Push(op);
        public string UltimaOperazione() => operazioni.Count > 0 ? operazioni.Peek() : null;
        public string Annulla() => operazioni.Count > 0 ? operazioni.Pop() : null;
        public List<string> OttieniTutte() => new List<string>(operazioni);
        public int Count => operazioni.Count;
    }
}
