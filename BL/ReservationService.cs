using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public class ReservationService
    {
        private ReservationCRUD _reservationCRUD;
        private readonly BookCRUD _bookCRUD;

        public ReservationService()
        {
            _bookCRUD = new BookCRUD();
            _reservationCRUD = new ReservationCRUD();
        }

        public DataTable GetAvailableBooks()
        {
            // Ottieni l'elenco dei libri disponibili dalla DAL
            return _bookCRUD.GetBooksTable();
        }

        public bool IsValidBookIndex(int selectedBookIndex, int bookCount)
        {
            // Verifica se l'indice selezionato è valido
            return selectedBookIndex >= 1 && selectedBookIndex <= bookCount;
        }

        public bool IsBookAvailable(DataRow selectedBookRow)
        {
            // Verifica se il libro è disponibile per la prenotazione
            int bookQuantity = (int)selectedBookRow["Quantity"];

            // Ottieni il numero di prenotazioni per questo libro
            int reservationCount = _reservationCRUD.GetReservationCountForBook((int)selectedBookRow["BookId"]);

            return bookQuantity > reservationCount;
        }

        public void CreateReservation(int selectedBookIndex)
        {
            // Ottieni l'ID dell'utente corrente
            int userId = CurrentUserManager.Instance.UserId;

            DataTable booksTable = GetAvailableBooks();

            // Ottieni il libro selezionato
            DataRow selectedBookRow = booksTable.Rows[selectedBookIndex - 1];
            int bookId = (int)selectedBookRow["BookId"];

            // Ottieni la data di inizio prenotazione dal CurrentUserManager
            DateTime startDate = CurrentUserManager.Instance.LoginTime;

            // Passa la prenotazione alla DAL per l'aggiunta nel dataset
            _reservationCRUD.AddReservation(new Reservation(userId, bookId, startDate));
        }
    }
}


