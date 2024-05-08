using System;
using System.Data;
using BL;
using Model;


namespace BibliotecaApp
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginManager loginManager = new LoginManager();
            bool loggedIn = false;
            int userId = -1; // Inizializza l'ID dell'utente a un valore non valido
            UserRole role = UserRole.User; // Inizializza il ruolo dell'utente a user
            string username = string.Empty; // Inizializza il nome utente a una stringa vuota

            while (!loggedIn)
            {
                Console.WriteLine("Benvenuto alla Biblioteca!");
                Console.Write("Username: ");
                username = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();

                // Effettua il controllo del login utilizzando il gestore di login
                (userId, role, string statusMessage) = loginManager.CheckLogin(username, password);
                Console.WriteLine(statusMessage);

                // Se l'ID dell'utente è valido, il login ha avuto successo
                loggedIn = userId != -1;

                // Se il login ha successo, esci dal loop di autenticazione
                if (loggedIn)
                {
                    // Saluto personalizzato in base al ruolo dell'utente
                    if (role == UserRole.Admin)
                    {
                        Console.WriteLine($"Benvenuto, {username}! Sei nel pannello amministratore.");
                    }
                    else if (role == UserRole.User)
                    {
                        Console.WriteLine($"Benvenuto, {username}!");
                    }
                }
            }



            // Dopo l'accesso, mostra il menu delle opzioni disponibili in base al ruolo dell'utente
            while (true)
            {
                Console.WriteLine("\nMenu:");

                if (role == UserRole.Admin)
                {
                    // Mostra il menu per l'amministratore
                    Console.WriteLine("1. Aggiungi Libro");
                    Console.WriteLine("2. Elimina Libro");
                    Console.WriteLine("3. Aggiorna Libro");
                }
                else if (role == UserRole.User)
                {
                    // Mostra il menu per l'utente normale (aggiungi ulteriori opzioni qui)
                    Console.WriteLine("1. Visualizza Libri");
                    Console.WriteLine("2. Effettua Prenotazione");
                    Console.WriteLine("3. Visualizza Prenotazioni");
                }

                Console.WriteLine("4. Esci");

                Console.Write("\nSelezione: ");
                string choice = Console.ReadLine();
                BookService bookService = new BookService();
                InterazioniPrenotazioni interazioniPrenotazioni = new InterazioniPrenotazioni();
                InterazioniLibri interazioniLibri = new InterazioniLibri();

                switch (choice)
                {
                    case "1":
                        if (role == UserRole.Admin)
                            interazioniLibri.AggiuntaLibroManuale();
                        else if (role == UserRole.User)
                            Console.WriteLine("Opzione non ancora implementata per gli utenti.");
                        break;
                    case "2":
                        if (role == UserRole.Admin)
                            interazioniLibri.EliminaLibroManuale();
                        else if (role == UserRole.User)
                            interazioniPrenotazioni.PrenotazioneLibro();
                        break;
                    case "3":
                        if (role == UserRole.Admin)
                            interazioniLibri.AggiornaLibroManuale();
                        else if (role == UserRole.User)
                            Console.WriteLine("Opzione non ancora implementata per gli utenti.");
                        break;
                    case "4":
                        // Esci dal programma sia se l'utente è un admin che un utente normale
                        Console.WriteLine("Arrivederci!");
                        return;
                    default:
                        Console.WriteLine("Selezione non valida. Riprova.");
                        break;
                }
            }
        }
    }
}

