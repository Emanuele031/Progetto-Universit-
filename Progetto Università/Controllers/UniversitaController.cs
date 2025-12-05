using System;
using System.Collections.Generic;
using Models;
using Repositories;
using Services;

namespace Controllers
{
    public class UniversitaController
    {
        private RepositoryGenerico<Studente> studentiRepo = new RepositoryGenerico<Studente>();
        private RepositoryGenerico<Professore> professoriRepo = new RepositoryGenerico<Professore>();
        private RepositoryGenerico<CorsoDiLaurea> corsiRepo = new RepositoryGenerico<CorsoDiLaurea>();

        private StoricoOperazioni storico = new StoricoOperazioni();
        private CodaIscrizioni codaIscrizioni = new CodaIscrizioni();

        public UniversitaController()
        {
            InizializzaCorsiEProfessori();
        }

        private void InizializzaCorsiEProfessori()
        {
            var corsoINF = new CorsoDiLaurea("INF", "Informatica");
            var corsoMAT = new CorsoDiLaurea("MAT", "Matematica");
            var corsoFIS = new CorsoDiLaurea("FIS", "Fisica");

            corsiRepo.Aggiungi(corsoINF);
            corsiRepo.Aggiungi(corsoMAT);
            corsiRepo.Aggiungi(corsoFIS);

            
            var materieProf1 = new List<string> { "Programmazione", "Basi di Dati" };
            var materieProf2 = new List<string> { "Reti", "Sicurezza Informatica" };
            var materieProf3 = new List<string> { "Analisi Matematica", "Algebra" };
            var materieProf4 = new List<string> { "Fisica 1", "Fisica 2" };

            
            var prof1 = new Professore("Mario", "Rossi", "P01", materieProf1);
            var prof2 = new Professore("Luigi", "Verdi", "P02", materieProf2);
            var prof3 = new Professore("Anna", "Bianchi", "P03", materieProf3);
            var prof4 = new Professore("Carlo", "Neri", "P04", materieProf4);

            
            corsoINF.AggiungiProfessore(prof1, materieProf1);
            corsoINF.AggiungiProfessore(prof2, materieProf2);
            corsoMAT.AggiungiProfessore(prof3, materieProf3);
            corsoFIS.AggiungiProfessore(prof4, materieProf4);

            
            professoriRepo.Aggiungi(prof1);
            professoriRepo.Aggiungi(prof2);
            professoriRepo.Aggiungi(prof3);
            professoriRepo.Aggiungi(prof4);

            storico.Registra("[INIZIALIZZAZIONE] Corsi e professori predefiniti aggiunti.");
        }



        public void AggiungiStudente(Studente s)
        {
            studentiRepo.Aggiungi(s);
            storico.Registra($"[STUDENTE] {s.Nome} {s.Cognome} aggiunto.");
        }

        public Studente CercaStudente(string matricola)
        {
            var s = studentiRepo.Cerca(matricola);
            if (s == null) throw new Exception("Studente non trovato.");
            return s;
        }

        public void AggiungiVotoStudente(string matricola, string materia, int valore)
        {
            var s = CercaStudente(matricola);
            s.AggiungiVoto(new Voto(materia, valore));
            storico.Registra($"[VOTO] {valore} in {materia} aggiunto a {s.Nome} {s.Cognome}.");
        }

        public void StampaLibrettoStudente(string matricola)
        {
            var s = CercaStudente(matricola);
            s.StampaLibretto();
            storico.Registra($"[LIBRETTO] Visualizzato libretto di {s.Nome} {s.Cognome}.");
        }

        public List<Studente> OrdinaStudentiPerMedia()
        {
            var lista = studentiRepo.Tutti();
            lista.Sort();
            storico.Registra("[AMMINISTRAZIONE] Studenti ordinati per media.");
            return lista;
        }

        
        public void AggiungiProfessore(Professore p, string codiceCorso, List<string> materie)
        {
            var corso = corsiRepo.Cerca(codiceCorso) ?? throw new Exception("Corso non trovato.");
            corso.AggiungiProfessore(p, materie);
            professoriRepo.Aggiungi(p);
            storico.Registra($"[PROFESSORE] {p.Nome} {p.Cognome} aggiunto al corso {corso.Nome} con materie: {string.Join(", ", materie)}");
        }

        
        public void AggiungiCorso(CorsoDiLaurea corso)
        {
            corsiRepo.Aggiungi(corso);
            storico.Registra($"[CORSO] {corso.Nome} aggiunto.");
        }

        public CorsoDiLaurea CercaCorso(string codice)
        {
            var c = corsiRepo.Cerca(codice);
            if (c == null) throw new Exception("Corso non trovato.");
            return c;
        }

        public List<CorsoDiLaurea> CercaTuttiCorsi() => corsiRepo.Tutti();
        public List<Professore> CercaTuttiProfessori() => professoriRepo.Tutti();
        public List<Studente> CercaTuttiStudenti() => studentiRepo.Tutti();

        
        public List<string> VisualizzaStorico() => storico.OttieniTutte();

        public string AnnullaUltimaOperazione()
        {
            var op = storico.Annulla();
            if (op != null) storico.Registra($"[AMMINISTRAZIONE] Operazione annullata: {op}");
            return op;
        }

        
        public void AggiungiRichiestaIscrizione(Studente s, CorsoDiLaurea corso)
        {
            codaIscrizioni.AggiungiRichiesta(new RichiestaIscrizione(s, corso));
            storico.Registra($"[AMMINISTRAZIONE] Richiesta iscrizione aggiunta: {s.Nome} {s.Cognome} -> {corso.Nome}");
        }

        public RichiestaIscrizione ApprovaProssimaIscrizione()
        {
            var r = codaIscrizioni.ApprovaProssima();
            if (r != null)
            {
                r.Studente.AggiungiCorsoAlLibretto(r.Corso);
                storico.Registra($"[AMMINISTRAZIONE] Iscrizione approvata: {r.Studente.Nome} {r.Studente.Cognome} -> {r.Corso.Nome}");
            }
            return r;
        }

        public List<RichiestaIscrizione> VisualizzaCodaIscrizioni()
        {
            var lista = codaIscrizioni.OttieniRichiesteInAttesa();
            storico.Registra("[AMMINISTRAZIONE] Visualizzata coda iscrizioni.");
            return lista;
        }
    }
}
//mailto:f.lombardi79@gmail.com
/*Una classe POCO (Plain Old CLass) è una classe che rappresenta un semplice oggetto di dominio, senza dipendenze da framework complessi, e che viene utilizzata principalmente per il recupero e la memorizzazione di dati. È un concetto chiave nella programmazione a oggetti, specialmente in linguaggi come Java e C#, e la sua semplicità la rende facilmente utilizzabile per serializzazione, deserializzazione e per passare dati tra diversi strati di un'applicazione. 
Caratteristiche principali di una classe POCO:
Semplicità: Non ha bisogno di ereditare da classi di framework specifiche o di implementare interfacce speciali.
Focus sui dati: La sua funzione principale è quella di contenere dati, con proprietà pubbliche e metodi getter/setter.
Indipendenza: È libera da dipendenze esterne complesse, il che la rende riutilizzabile e testabile in modo isolato.
Versatilità: È ideale per l'interscambio di dati e per la persistenza in database o file*/