using System;
using Models;
using Controllers;

namespace Views
{
    public static class Menu
    {
        public static void Start(UniversitaController uni)
        {
            int scelta;
            do
            {
                Console.Clear();
                Console.WriteLine("===== GESTIONE UNIVERSITÀ =====");
                Console.WriteLine("1. Aggiungi corso di laurea");
                Console.WriteLine("2. Aggiungi professore");
                Console.WriteLine("3. Aggiungi studente");
                Console.WriteLine("4. Aggiungi voto a studente");
                Console.WriteLine("5. Visualizza libretto studente");
                Console.WriteLine("6. Visualizza tutti i corsi");
                Console.WriteLine("7. Visualizza tutti i professori");
                Console.WriteLine("8. Visualizza tutti gli studenti");
                Console.WriteLine("9. Esci");
                Console.WriteLine("10. Menu Amministrazione");
                Console.Write("Seleziona: ");

                if (!int.TryParse(Console.ReadLine(), out scelta))
                {
                    Console.WriteLine("Input non valido.");
                    Console.ReadKey();
                    continue;
                }

                try
                {
                    if (scelta >= 1 && scelta <= 9)
                        EseguiSceltaPrincipale(uni, scelta);
                    else if (scelta == 10)
                        MenuAmministrazione(uni);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Errore: {ex.Message}");
                }

                if (scelta != 9)
                {
                    Console.WriteLine("\nPremi un tasto per continuare...");
                    Console.ReadKey();
                }

            } while (scelta != 9);
        }

        static void EseguiSceltaPrincipale(UniversitaController uni, int scelta)
        {
            switch (scelta)
            {
                case 1: AggiungiCorso(uni); break;
                case 2: AggiungiProfessore(uni); break;
                case 3: AggiungiStudente(uni); break;
                case 4: AggiungiVoto(uni); break;
                case 5: VisualizzaLibretto(uni); break;
                case 6: VisualizzaCorsi(uni); break;
                case 7: VisualizzaProfessori(uni); break;
                case 8: VisualizzaStudenti(uni); break;
                case 9: break;
            }
        }

        static void MenuAmministrazione(UniversitaController uni)
        {
            int sceltaAdmin;
            do
            {
                Console.Clear();
                Console.WriteLine("===== MENU AMMINISTRAZIONE =====");
                Console.WriteLine("1. Ordina studenti per media");
                Console.WriteLine("2. Visualizza storico operazioni");
                Console.WriteLine("3. Annulla ultima operazione");
                Console.WriteLine("4. Aggiungi richiesta iscrizione");
                Console.WriteLine("5. Approva prossima iscrizione");
                Console.WriteLine("6. Visualizza coda iscrizioni");
                Console.WriteLine("7. Torna al menu principale");
                Console.Write("Seleziona: ");

                if (!int.TryParse(Console.ReadLine(), out sceltaAdmin))
                {
                    Console.WriteLine("Input non valido.");
                    Console.ReadKey();
                    continue;
                }

                try
                {
                    switch (sceltaAdmin)
                    {
                        case 1: OrdinaStudenti(uni); break;
                        case 2: VisualizzaStorico(uni); break;
                        case 3: AnnullaUltimaOperazione(uni); break;
                        case 4: AggiungiRichiestaIscrizione(uni); break;
                        case 5: ApprovaProssimaIscrizione(uni); break;
                        case 6: VisualizzaCodaIscrizioni(uni); break;
                        case 7: return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Errore: {ex.Message}");
                }

                Console.WriteLine("\nPremi un tasto per continuare...");
                Console.ReadKey();

            } while (sceltaAdmin != 7);
        }

        // ----- Metodi principali -----
        static void AggiungiCorso(UniversitaController uni)
        {
            Console.Write("Codice corso: "); string codice = Console.ReadLine();
            Console.Write("Nome corso: "); string nome = Console.ReadLine();
            uni.AggiungiCorso(new CorsoDiLaurea(codice, nome));
            Console.WriteLine("Corso aggiunto.");
        }

        static void AggiungiProfessore(UniversitaController uni)
        {
            Console.Write("Nome: "); string nome = Console.ReadLine();
            Console.Write("Cognome: "); string cognome = Console.ReadLine();
            Console.Write("Codice ID: "); string id = Console.ReadLine();
            Console.Write("Materia: "); string materia = Console.ReadLine();
            Console.Write("Codice corso: "); string corso = Console.ReadLine();
            uni.AggiungiProfessore(new Professore(nome, cognome, id, materia), corso);
            Console.WriteLine("Professore aggiunto.");
        }

        static void AggiungiStudente(UniversitaController uni)
        {
            Console.Write("Nome: "); string nome = Console.ReadLine();
            Console.Write("Cognome: "); string cognome = Console.ReadLine();
            Console.Write("Matricola: "); string matricola = Console.ReadLine();
            Console.WriteLine("Corsi disponibili:");
            foreach (var c in uni.CercaTuttiCorsi()) Console.WriteLine($"{c.Codice} - {c.Nome}");
            Console.Write("Codice corso: "); string codiceCorso = Console.ReadLine();
            var corso = uni.CercaCorso(codiceCorso);
            uni.AggiungiStudente(new Studente(nome, cognome, matricola, corso));
            Console.WriteLine("Studente aggiunto.");
        }

        static void AggiungiVoto(UniversitaController uni)
        {
            Console.Write("Matricola: "); string matricola = Console.ReadLine();
            Console.Write("Materia: "); string materia = Console.ReadLine();
            Console.Write("Voto: ");
            if (!int.TryParse(Console.ReadLine(), out int voto)) { Console.WriteLine("Voto non valido."); return; }
            uni.AggiungiVotoStudente(matricola, materia, voto);
            Console.WriteLine("Voto aggiunto.");
        }

        static void VisualizzaLibretto(UniversitaController uni)
        {
            Console.Write("Matricola: "); string matricola = Console.ReadLine();
            uni.StampaLibrettoStudente(matricola);
        }

        static void VisualizzaCorsi(UniversitaController uni)
        {
            Console.WriteLine("--- Corsi ---");
            foreach (var c in uni.CercaTuttiCorsi()) Console.WriteLine(c);
        }

        static void VisualizzaProfessori(UniversitaController uni)
        {
            Console.WriteLine("--- Professori ---");
            foreach (var p in uni.CercaTuttiProfessori()) Console.WriteLine(p);
        }

        static void VisualizzaStudenti(UniversitaController uni)
        {
            Console.WriteLine("--- Studenti ---");
            foreach (var s in uni.CercaTuttiStudenti())
                Console.WriteLine($"{s.Nome} {s.Cognome} ({s.Matricola}) - Corso: {s.CorsoIscritto.Nome}");
        }

        // ----- Metodi amministrazione -----
        static void OrdinaStudenti(UniversitaController uni)
        {
            Console.WriteLine("--- Studenti ordinati per media ---");
            foreach (var s in uni.OrdinaStudentiPerMedia())
                Console.WriteLine($"{s.Nome} {s.Cognome} ({s.Matricola}) - Media: {s.Media:F2}");
        }

        static void VisualizzaStorico(UniversitaController uni)
        {
            Console.WriteLine("--- Storico Operazioni ---");
            foreach (var op in uni.VisualizzaStorico()) Console.WriteLine(op);
        }

        static void AnnullaUltimaOperazione(UniversitaController uni)
        {
            var op = uni.AnnullaUltimaOperazione();
            Console.WriteLine(op != null ? $"Operazione annullata: {op}" : "Nessuna operazione da annullare.");
        }

        static void AggiungiRichiestaIscrizione(UniversitaController uni)
        {
            Console.Write("Matricola studente: "); string m = Console.ReadLine();
            Console.WriteLine("Corsi disponibili:");
            foreach (var c in uni.CercaTuttiCorsi()) Console.WriteLine($"{c.Codice} - {c.Nome}");
            Console.Write("Codice corso: "); string codiceCorso = Console.ReadLine();
            var corso = uni.CercaCorso(codiceCorso);
            var studente = uni.CercaStudente(m);
            uni.AggiungiRichiestaIscrizione(studente, corso);
            Console.WriteLine("Richiesta aggiunta alla coda.");
        }

        static void ApprovaProssimaIscrizione(UniversitaController uni)
        {
            var r = uni.ApprovaProssimaIscrizione();
            Console.WriteLine(r != null ? $"Iscrizione approvata: {r.Studente.Nome} {r.Studente.Cognome} -> {r.Corso.Nome}" : "Nessuna richiesta in coda.");
        }

        static void VisualizzaCodaIscrizioni(UniversitaController uni)
        {
            Console.WriteLine("--- Coda iscrizioni ---");
            foreach (var r in uni.VisualizzaCodaIscrizioni())
                Console.WriteLine($"{r.Studente.Nome} {r.Studente.Cognome} ({r.Studente.Matricola}) -> {r.Corso.Nome}");
        }
    }
}
