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

        public UniversitaController() { InizializzaCorsi(); }

        private void InizializzaCorsi()
        {
            var corso1 = new CorsoDiLaurea("INF", "Informatica");
            var corso2 = new CorsoDiLaurea("MAT", "Matematica");
            var corso3 = new CorsoDiLaurea("FIS", "Fisica");

            corsiRepo.Aggiungi(corso1);
            corsiRepo.Aggiungi(corso2);
            corsiRepo.Aggiungi(corso3);

            storico.Registra("[INIZIALIZZAZIONE] Corsi predefiniti aggiunti.");
        }

        // --- Studenti ---
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

        // --- Professori ---
        public void AggiungiProfessore(Professore p, string codiceCorso)
        {
            var corso = corsiRepo.Cerca(codiceCorso) ?? throw new Exception("Corso non trovato.");
            corso.AggiungiProfessore(p);
            professoriRepo.Aggiungi(p);
            storico.Registra($"[PROFESSORE] {p.Nome} {p.Cognome} aggiunto al corso {corso.Nome}.");
        }

        // --- Corsi ---
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

        // --- Storico ---
        public List<string> VisualizzaStorico() => storico.OttieniTutte();

        public string AnnullaUltimaOperazione()
        {
            var op = storico.Annulla();
            if (op != null) storico.Registra($"[AMMINISTRAZIONE] Operazione annullata: {op}");
            return op;
        }

        // --- Coda iscrizioni ---
        public void AggiungiRichiestaIscrizione(Studente s, CorsoDiLaurea corso)
        {
            codaIscrizioni.AggiungiRichiesta(new RichiestaIscrizione(s, corso));
            storico.Registra($"[AMMINISTRAZIONE] Richiesta iscrizione aggiunta: {s.Nome} {s.Cognome} -> {corso.Nome}");
        }

        public RichiestaIscrizione ApprovaProssimaIscrizione()
        {
            var r = codaIscrizioni.ApprovaProssima();
            if (r != null)
                storico.Registra($"[AMMINISTRAZIONE] Iscrizione approvata: {r.Studente.Nome} {r.Studente.Cognome} -> {r.Corso.Nome}");
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
