using System.Collections.Generic;
using Models;

namespace Services
{
    public class CodaIscrizioni
    {
        private Queue<RichiestaIscrizione> coda = new Queue<RichiestaIscrizione>();

        public void AggiungiRichiesta(RichiestaIscrizione richiesta) => coda.Enqueue(richiesta);

        public RichiestaIscrizione ApprovaProssima() => coda.Count > 0 ? coda.Dequeue() : null;

        public RichiestaIscrizione ProssimoInCoda() => coda.Count > 0 ? coda.Peek() : null;

        public List<RichiestaIscrizione> OttieniRichiesteInAttesa() => new List<RichiestaIscrizione>(coda);

        public int NumeroRichieste => coda.Count;
    }
}
