using Controllers;
using Models;
using Progetto_Università.Interfaces;
using Services;
using System;
using System.Data.SqlClient;

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
                Console.WriteLine("11. Menu Logger");
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
                    else if (scelta == 11)
                        MenuLogger();
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


        static void MenuLogger()
        {
            int scelta;
            do
            {
                Console.Clear();
                Console.WriteLine("=== MENU LOGGER ===");
                Console.WriteLine("1. Visualizza log memoria");
                Console.WriteLine("2. Pulisci log memoria");
                Console.WriteLine("3. Visualizza log file");
                Console.WriteLine("4. Pulisci log file");
                Console.WriteLine("5. Visualizza log database"); 
                Console.WriteLine("6. Pulisci log database");    
                Console.WriteLine("7. Configura Logger (Attiva/Disattiva)"); 
                Console.WriteLine("8. Cambia percorso file log");            
                Console.WriteLine("9. Torna al menu principale");            
                Console.Write("Seleziona: ");

                if (!int.TryParse(Console.ReadLine(), out scelta))
                {
                    Console.WriteLine("Input non valido.");
                    Console.ReadKey();
                    continue;
                }

                switch (scelta)
                {
                    case 1:
                        Console.WriteLine("--- Log in memoria ---");
                        var memoriaLogs = Logger.Instance.GetLogs();
                        if (memoriaLogs.Count == 0)
                            Console.WriteLine("Nessun log presente in memoria.");
                        else
                            foreach (var msg in memoriaLogs)
                                Console.WriteLine(msg);
                        break;

                    case 2:
                        Logger.Instance.Clear();
                        Console.WriteLine("Log in memoria pulito.");
                        break;

                    case 3:
                        Console.WriteLine("--- Log su file ---");
                        var fileLogs = LoggerFile.Instance.GetLogs();
                        if (fileLogs.Count == 0)
                            Console.WriteLine("Nessun log presente nel file.");
                        else
                            foreach (var msg in fileLogs)
                                Console.WriteLine(msg);
                        break;

                    case 4:
                        LoggerFile.Instance.Clear();
                        Console.WriteLine("Log su file pulito.");
                        break;

                    case 5: 
                        Console.WriteLine("--- Log su Database ---");
                        var dbLogs = LoggerDatabase.Instance.GetLogs();
                        if (dbLogs.Count == 0)
                            Console.WriteLine("Nessun log presente nel database.");
                        else
                            foreach (var msg in dbLogs)
                                Console.WriteLine(msg);
                        break;

                    case 6: 
                        LoggerDatabase.Instance.Clear();
                        Console.WriteLine("Log su database pulito.");
                        break;

                    case 7: MenuConfigurazioneLogger(); break; 

                    case 8: 
                        Console.Write("Nuovo percorso file log: ");
                        string path = Console.ReadLine();
                        LoggerFile.Instance.SetFilePath(path);
                        Console.WriteLine($"Percorso log aggiornato a {path}");
                        break;

                    case 9: return;
                    default: Console.WriteLine("Scelta non valida"); break;
                }

                Console.WriteLine("\nPremi un tasto per continuare...");
                Console.ReadKey();

            } while (scelta != 9);
        }

        static void MenuConfigurazioneLogger()
        {
            int scelta;
            do
            {
                Console.Clear();
                Console.WriteLine("=== CONFIGURAZIONE LOGGER ===");
                Console.WriteLine("Quale logger vuoi attivare/disattivare?");

                
                bool statoMemoria = CompositeLogger.Instance.GetLoggerStatus(Logger.Instance);
                bool statoFile = CompositeLogger.Instance.GetLoggerStatus(LoggerFile.Instance);
                bool statoDb = CompositeLogger.Instance.GetLoggerStatus(LoggerDatabase.Instance);

                Console.WriteLine($"1. Logger in Memoria ({(statoMemoria ? "ATTIVO" : "DISATTIVO")})");
                Console.WriteLine($"2. Logger su File ({(statoFile ? "ATTIVO" : "DISATTIVO")})");
                Console.WriteLine($"3. Logger su Database ({(statoDb ? "ATTIVO" : "DISATTIVO")})");
                Console.WriteLine("4. Torna al menu Logger");
                Console.Write("Seleziona: ");

                if (!int.TryParse(Console.ReadLine(), out scelta))
                {
                    Console.WriteLine("Input non valido.");
                    Console.ReadKey();
                    continue;
                }

                ILogger loggerSelezionato = null;
                string nomeLogger = "";

                switch (scelta)
                {
                    case 1: loggerSelezionato = Logger.Instance; nomeLogger = "Memoria"; break;
                    case 2: loggerSelezionato = LoggerFile.Instance; nomeLogger = "File"; break;
                    case 3: loggerSelezionato = LoggerDatabase.Instance; nomeLogger = "Database"; break;
                    case 4: return;
                    default: Console.WriteLine("Scelta non valida."); break;
                }

                if (loggerSelezionato != null)
                {
                    bool statoAttuale = CompositeLogger.Instance.GetLoggerStatus(loggerSelezionato);
                    Console.WriteLine($"\nLo stato attuale del Logger {nomeLogger} è: {(statoAttuale ? "ATTIVO" : "DISATTIVO")}");
                    Console.Write("Vuoi (A)ttivarlo o (D)isattivarlo? (A/D): ");
                    string azione = Console.ReadLine().ToUpper();

                    if (azione == "A")
                    {
                        CompositeLogger.Instance.SetLoggerStatus(loggerSelezionato, true);
                        Console.WriteLine($"Logger {nomeLogger} ATTIVATO.");
                    }
                    else if (azione == "D")
                    {
                        CompositeLogger.Instance.SetLoggerStatus(loggerSelezionato, false);
                        Console.WriteLine($"Logger {nomeLogger} DISATTIVATO.");
                    }
                    else
                    {
                        Console.WriteLine("Azione non valida. Ritorno al menu precedente.");
                    }
                }

                Console.WriteLine("\nPremi un tasto per continuare...");
                Console.ReadKey();

            } while (scelta != 4);
        }

        static void AggiungiCorso(UniversitaController uni)
        {
            Console.Write("Codice corso: "); string codice = Console.ReadLine();
            Console.Write("Nome corso: "); string nome = Console.ReadLine();
            uni.AggiungiCorso(new CorsoDiLaurea(codice, nome));
            string msg = $"[LOG] Corso aggiunto: {nome} ({codice})";
            
            CompositeLogger.Instance.Log(msg);
            Console.WriteLine("Corso aggiunto.");
        }

        static void AggiungiProfessore(UniversitaController uni)
        {
            Console.Write("Nome: "); string nome = Console.ReadLine();
            Console.Write("Cognome: "); string cognome = Console.ReadLine();
            Console.Write("Codice ID: "); string id = Console.ReadLine();

            Console.WriteLine("Corsi disponibili:");
            foreach (var c in uni.CercaTuttiCorsi())
                Console.WriteLine($"{c.Codice} - {c.Nome}");

            Console.Write("Codice corso: "); string codiceCorso = Console.ReadLine();
            var corso = uni.CercaCorso(codiceCorso);
            if (corso == null)
            {
                Console.WriteLine("Corso non trovato.");
                return;
            }

            Console.Write("Materia insegnata: "); string materia = Console.ReadLine();
            var materie = new List<string> { materia };

            Professore p = new Professore(nome, cognome, id, materie);

            uni.AggiungiProfessore(p, corso.Codice, materie);

            string msg = $"[LOG] Professore aggiunto: {nome} {cognome} ({id}) - Materia: {materia} al corso {corso.Nome}";
            CompositeLogger.Instance.Log(msg);


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
            string msg = $"[LOG] Studente aggiunto: {nome} {cognome} ({matricola})";
            CompositeLogger.Instance.Log(msg);
            Console.WriteLine("Studente aggiunto.");
        }

        static void AggiungiVoto(UniversitaController uni)
        {
            Console.Write("Matricola: "); string matricola = Console.ReadLine();
            Console.Write("Materia: "); string materia = Console.ReadLine();
            Console.Write("Voto: ");
            if (!int.TryParse(Console.ReadLine(), out int voto)) { Console.WriteLine("Voto non valido."); return; }
            uni.AggiungiVotoStudente(matricola, materia, voto);
            string msg = $"[LOG] Voto aggiunto: {voto} in {materia} per {matricola}";
            CompositeLogger.Instance.Log(msg);
            Console.WriteLine("Voto aggiunto.");
        }

        static void VisualizzaLibretto(UniversitaController uni)
        {
            Console.Write("Matricola: "); string matricola = Console.ReadLine();
            uni.StampaLibrettoStudente(matricola);
            string msg = $"[LOG] Visualizzato libretto studente: {matricola}";
            CompositeLogger.Instance.Log(msg);
        }

        
        static void OrdinaStudenti(UniversitaController uni)
        {
            Console.WriteLine("--- Studenti ordinati per media ---");
            foreach (var s in uni.OrdinaStudentiPerMedia())
                Console.WriteLine($"{s.Nome} {s.Cognome} ({s.Matricola}) - Media: {s.Media:F2}");
            string msg = "[LOG] Studenti ordinati per media";
            CompositeLogger.Instance.Log(msg);
        }

        static void VisualizzaStorico(UniversitaController uni)
        {
            Console.WriteLine("--- Storico Operazioni ---");
            foreach (var op in uni.VisualizzaStorico()) Console.WriteLine(op);
            string msg = "[LOG] Visualizzato storico operazioni";
            CompositeLogger.Instance.Log(msg);
        }

        static void AnnullaUltimaOperazione(UniversitaController uni)
        {
            var op = uni.AnnullaUltimaOperazione();
            Console.WriteLine(op != null ? $"Operazione annullata: {op}" : "Nessuna operazione da annullare.");
            string msg = $"[LOG] Operazione annullata: {op}";
            CompositeLogger.Instance.Log(msg);
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
            string msg = $"[LOG] Richiesta iscrizione aggiunta: {m} -> {codiceCorso}";
            CompositeLogger.Instance.Log(msg);
            Console.WriteLine("Richiesta aggiunta alla coda.");
        }

        static void ApprovaProssimaIscrizione(UniversitaController uni)
        {
            var r = uni.ApprovaProssimaIscrizione();
            Console.WriteLine(r != null ? $"Iscrizione approvata: {r.Studente.Nome} {r.Studente.Cognome} -> {r.Corso.Nome}" : "Nessuna richiesta in coda.");
            string msg = r != null ? $"[LOG] Iscrizione approvata: {r.Studente.Matricola} -> {r.Corso.Codice}" : "[LOG] Nessuna iscrizione da approvare";
            CompositeLogger.Instance.Log(msg);
        }

        static void VisualizzaCodaIscrizioni(UniversitaController uni)
        {
            Console.WriteLine("--- Coda iscrizioni ---");
            foreach (var r in uni.VisualizzaCodaIscrizioni())
                Console.WriteLine($"{r.Studente.Nome} {r.Studente.Cognome} ({r.Studente.Matricola}) -> {r.Corso.Nome}");
            string msg = "[LOG] Visualizzata coda iscrizioni";
            CompositeLogger.Instance.Log(msg);
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
    }
}
