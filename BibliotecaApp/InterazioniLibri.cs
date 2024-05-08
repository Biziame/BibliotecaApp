using BL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaApp
{
    class InterazioniLibri
    {
        private BookService _bookservice;

        public InterazioniLibri()
        {
            _bookservice = new BookService();
        }

        public void AggiuntaLibroManuale()
        {
            string title, authorName, authorSurname, publishingHouse;

            // Continua a richiedere l'input finché tutti i campi non sono stati inseriti correttamente
            while (true)
            {
                Console.WriteLine("Titolo:");
                title = Console.ReadLine().Trim();
                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("Il titolo non può essere vuoto. Inserisci nuovamente il titolo.");
                    continue;
                }

                // Controlla se il libro esiste già
                if (_bookservice.CheckIfBookExists(title, out Book existingBook))
                {
                    Console.WriteLine($"Il libro '{existingBook.Title}' di {existingBook.AuthorName} {existingBook.AuthorSurname} della casa editrice '{existingBook.PublishingHouse}' esiste già.");

                    // Chiedi all'utente se vuole aggiungere una nuova copia
                    Console.WriteLine("Vuoi aggiungere una nuova copia di questo libro? (Sì/No)");
                    string response = Console.ReadLine().Trim().ToLower();

                    if (response == "sì" || response == "si")
                    {
                        // Aggiungi una copia del libro esistente
                        _bookservice.AggiungiCopiaLibro(existingBook);
                        Console.WriteLine("Nuova copia del libro aggiunta con successo!");
                        return; // Esci dal metodo dopo l'aggiunta della copia
                    }
                    else
                    {
                        Console.WriteLine("Operazione annullata.");
                        return; // Esci dal metodo se l'utente decide di annullare l'operazione
                    }
                }
                else
                {
                    break; // Esci dal ciclo se il libro non esiste
                }
            }

            // Continua con l'aggiunta del libro
            Console.WriteLine("Nome dell'autore:");
            authorName = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(authorName))
            {
                Console.WriteLine("Il nome dell'autore non può essere vuoto. Inserisci nuovamente il nome dell'autore.");
                return;
            }

            Console.WriteLine("Cognome dell'autore:");
            authorSurname = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(authorSurname))
            {
                Console.WriteLine("Il cognome dell'autore non può essere vuoto. Inserisci nuovamente il cognome dell'autore.");
                return;
            }

            Console.WriteLine("Casa editrice:");
            publishingHouse = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(publishingHouse))
            {
                Console.WriteLine("Il nome della casa editrice non può essere vuoto. Inserisci nuovamente il nome della casa editrice.");
                return;
            }

            // Se tutti i campi sono stati inseriti correttamente, continua con l'aggiunta del libro
            int quantity = 1; // Default a 1 per una nuova aggiunta
            Book newBook = new Book()
            {
                Title = title,
                AuthorName = authorName,
                AuthorSurname = authorSurname,
                PublishingHouse = publishingHouse,
                Quantity = quantity
            };

            _bookservice.AggiungiLibro(newBook);
            Console.WriteLine("Libro aggiunto con successo!");
            return;
        }

        public void EliminaLibroManuale()
        {
            DataTable booksTable = _bookservice.ListaLibri();

            Console.WriteLine("Elenco dei libri:");
            for (int i = 0; i < booksTable.Rows.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {booksTable.Rows[i]["Title"]} - {booksTable.Rows[i]["AuthorName"]} {booksTable.Rows[i]["AuthorSurname"]}");
            }

            Console.WriteLine("Inserisci il numero del libro che desideri eliminare:");
            if (!int.TryParse(Console.ReadLine(), out int selectedBookIndex))
            {
                Console.WriteLine("Inserimento non valido.");
                return;
            }

            Book bookToDelete = _bookservice.LibroDaEliminare(selectedBookIndex);
            if (bookToDelete == null)
            {
                Console.WriteLine("Inserimento non valido.");
                return;
            }

            _bookservice.EliminaLibro(bookToDelete);
            Console.WriteLine("Libro eliminato con successo.");
        }

        public void AggiornaLibroManuale()
        {
            DataTable booksTable = _bookservice.ListaLibri();

            Console.WriteLine("Elenco dei libri:");
            for (int i = 0; i < booksTable.Rows.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {booksTable.Rows[i]["Title"]} - {booksTable.Rows[i]["AuthorName"]} " +
                    $"{booksTable.Rows[i]["AuthorSurname"]} - {booksTable.Rows[i]["PublishingHouse"]} | quantità: {booksTable.Rows[i]["Quantity"]} ");
            }

            Console.WriteLine("Inserisci il numero del libro che desideri aggiornare:");
            if (!int.TryParse(Console.ReadLine(), out int selectedBookIndex) || selectedBookIndex < 1 || selectedBookIndex > booksTable.Rows.Count)
            {
                Console.WriteLine("Inserimento non valido.");
                return;
            }


            Book bookToUpdate = _bookservice.LibroDaAggiornare(selectedBookIndex);
            if (bookToUpdate == null)
            {
                Console.WriteLine("Inserimento non valido.");
                return;
            }

            bool continuaModifica = true;
            while (continuaModifica)
            {
                Console.WriteLine("Inserisci il numero del campo che desideri modificare:");
                Console.WriteLine("1. Titolo");
                Console.WriteLine("2. Nome dell'autore");
                Console.WriteLine("3. Cognome dell'autore");
                Console.WriteLine("4. Casa editrice");
                //Console.WriteLine("5. Quantità");

                if (!int.TryParse(Console.ReadLine(), out int selectedFieldIndex) || selectedFieldIndex < 1 || selectedFieldIndex > 5)
                {
                    Console.WriteLine("Scelta non valida.");
                    return;
                }

                string fieldValue;
                switch (selectedFieldIndex)
                {
                    case 1:
                        Console.WriteLine("Inserisci il nuovo titolo:");
                        fieldValue = Console.ReadLine();
                        bookToUpdate.Title = fieldValue;
                        Console.WriteLine("Titolo del libro modificato con successo!");
                        break;
                    case 2:
                        Console.WriteLine("Inserisci il nuovo nome dell'autore:");
                        fieldValue = Console.ReadLine();
                        bookToUpdate.AuthorName = fieldValue;
                        Console.WriteLine("Nome dell'autore modificato con successo!");
                        break;
                    case 3:
                        Console.WriteLine("Inserisci il nuovo cognome dell'autore:");
                        fieldValue = Console.ReadLine();
                        bookToUpdate.AuthorSurname = fieldValue;
                        Console.WriteLine("Cognome dell'autore modificato con successo!");
                        break;
                    case 4:
                        Console.WriteLine("Inserisci la nuova casa editrice:");
                        fieldValue = Console.ReadLine();
                        bookToUpdate.PublishingHouse = fieldValue;
                        Console.WriteLine("Casa editrice modificata con successo!");
                        break;
                        /*
                    case 5:
                        Console.WriteLine("Inserisci la nuova quantità:");
                        if (!int.TryParse(Console.ReadLine(), out int quantity))
                        {
                            Console.WriteLine("Quantità non valida. Assicurati di inserire un numero intero.");
                            return;
                        }
                        bookToUpdate.Quantity = quantity;
                        Console.WriteLine("Quantità modificata con successo!");
                        break;
                        */
                }

                // Chiedi all'admin se vuole continuare a modificare altre voci del libro
                Console.WriteLine("Vuoi modificare altre voci del libro? (Sì/No)");
                string continuaInput = Console.ReadLine().Trim().ToLower();

                if (continuaInput != "sì" && continuaInput != "si")
                {
                    continuaModifica = false;
                }


            }

            _bookservice.AggiornaLibro(bookToUpdate);
            Console.WriteLine("Libro aggiornato con successo.");

        }

        


    }

}
