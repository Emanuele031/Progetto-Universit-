Questo progetto di Gestione Universitaria in C# è stato sviluppato con l’obiettivo di creare un’applicazione ordinata, scalabile e semplice da mantenere. 
Per farlo ho adottato un’architettura strutturata a strati, così da separare chiaramente responsabilità e logiche.
L’applicazione è suddivisa in cinque componenti principali.
I Models rappresentano i dati puri, come Studente e Professore, senza logica aggiuntiva. I Repositories si occupano della gestione delle collezioni in memoria e, grazie a un 
RepositoryGenerico<T>, è possibile riutilizzare la stessa struttura per qualsiasi entità. I Controllers funzionano da coordinatori, ricevono i comandi dell’utente e orchestrano
le operazioni chiamando Repository e Services. I Services svolgono le funzioni specializzate, come il sistema di logging o la gestione delle strutture dati utilizzate per
amministrazione e operazioni avanzate. Infine, le Views offrono un’interfaccia testuale semplice e diretta per l’interazione con l’utente.

Una parte fondamentale del progetto è il sistema di logging. Ho implementato più tipi di logger (memoria, file, database), ognuno gestito tramite il pattern Singleton per
garantirne una singola istanza. Tutti seguono l’interfaccia ILogger, e il CompositeLogger funge da punto di controllo centralizzato: permette di accendere o spegnere ogni
logger e inviare i messaggi solo a quelli attivi.

Per le funzionalità amministrative ho utilizzato strutture dati progettate per risolvere problemi specifici. Una pila (Stack) viene usata per l’operazione di annullamento,
mentre una coda (Queue) gestisce le richieste di iscrizione. L’uso di generics migliora notevolmente la capacità di riutilizzo del codice e riduce la duplicazione, mentre
l’interfaccia IComparable, applicata agli studenti, consente di definirne facilmente il criterio di ordinamento.

In generale, ogni componente svolge un ruolo chiaro e ben definito, creando un’applicazione solida, leggibile e organizzata. L’obiettivo è stato costruire un progetto
mantenibile e coerente, capace di mostrare una buona struttura e un uso consapevole dei principi di progettazione.
