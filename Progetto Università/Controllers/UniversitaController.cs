using System;
using System.Collections.Generic;
using Models;
using Interfaces;

namespace Controllers
{
    public class UniversitaController
    {
        private IStudenteRepository studentiRepo;
        private IProfessoreRepository professoriRepo;
        private ICorsoRepository corsiRepo;

        public UniversitaController(IStudenteRepository sr, IProfessoreRepository pr, ICorsoRepository cr)
        {
            studentiRepo = sr;
            professoriRepo = pr;
            corsiRepo = cr;
        }

        public void AggiungiStudente(Studente s) => studentiRepo.Aggiungi(s);
        public Studente CercaStudente(string matricola) => studentiRepo.CercaPerMatricola(matricola) ?? throw new Exception("Studente non trovato.");
        public void AggiungiVotoStudente(string matricola, string materia, int valore) => CercaStudente(matricola).AggiungiVoto(new Voto(materia, valore));
        public void StampaLibrettoStudente(string matricola) => CercaStudente(matricola).StampaLibretto();

        public void AggiungiProfessore(Professore p, string codiceCorso)
        {
            var corso = corsiRepo.CercaPerCodice(codiceCorso) ?? throw new Exception("Corso non trovato.");
            corso.AggiungiProfessore(p);
            professoriRepo.Aggiungi(p);
        }

        public void AggiungiCorso(CorsoDiLaurea corso) => corsiRepo.Aggiungi(corso);
        public CorsoDiLaurea CercaCorso(string codice) => corsiRepo.CercaPerCodice(codice);
        public List<CorsoDiLaurea> CercaTuttiCorsi() => corsiRepo.Tutti();
        public List<Professore> CercaTuttiProfessori() => professoriRepo.Tutti();
        public List<Studente> CercaTuttiStudenti() => studentiRepo.Tutti();
    }
}
