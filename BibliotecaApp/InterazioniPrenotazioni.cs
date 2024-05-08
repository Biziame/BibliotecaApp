using BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaApp
{
    class InterazioniPrenotazioni
    {
        private ReservationService _reservationService;

        public InterazioniPrenotazioni()
        {
            _reservationService = new ReservationService();
        }

        public void PrenotazioneLibro()
        {
            // Ottieni l'elenco dei libri disponibili
            DataTable booksTable = _reservationService.GetAvailableBooks();

            // Mostra all'utente l'elenco dei libri disponibili
            Console.WriteLine("Elenco dei libri disponibili:");
            for (int i = 0; i < booksTable.Rows.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {booksTable.Rows[i]["Title"]} - {booksTable.Rows[i]["AuthorName"]} {booksTable.Rows[i]["AuthorSurname"]}");
            }

            // Chiedi all'utente di selezionare il libro da prenotare
            Console.WriteLine("Inserisci il numero del libro che vuoi prenotare:");
            int selectedBookIndex = int.Parse(Console.ReadLine());

            // Verifica se l'indice selezionato è valido
            if (!_reservationService.IsValidBookIndex(selectedBookIndex, booksTable.Rows.Count))
            {
                Console.WriteLine("Indice libro non valido.");
                return;
            }

            // Ottieni il libro selezionato
            DataRow selectedBookRow = booksTable.Rows[selectedBookIndex - 1];

            // Verifica se il libro è disponibile per la prenotazione
            if (!_reservationService.IsBookAvailable(selectedBookRow))
            {
                Console.WriteLine("Il libro selezionato non è disponibile per la prenotazione.");
                return;
            }

            // Crea la prenotazione
            _reservationService.CreateReservation(selectedBookIndex);
            Console.WriteLine("Prenotazione creata con successo!");
        }
    }
}
