using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class ReservationCRUD

    {
        private LibraryDataSet _libraryDataSet;

        public ReservationCRUD()
        {
            _libraryDataSet = new LibraryDataSet();
        }


        // Metodo CRUD per la prenotazione
        public void AddReservation(Reservation reservation)
        {
            DataTable reservationsTable = _libraryDataSet.GetReservations();
            DataRow newRow = reservationsTable.NewRow();
            newRow["UserId"] = reservation.UserId;
            newRow["BookId"] = reservation.BookId;
            newRow["StartDate"] = reservation.StartDate;
            newRow["EndDate"] = reservation.EndDate; // Utilizza la data di fine fornita dall'oggetto Reservation
            reservationsTable.Rows.Add(newRow);
            _libraryDataSet.SaveChanges();
        }

        // Metodo CRUD per eliminare la prenotazione

        public void DeleteReservation(int reservationId)
        {
            DataTable reservationsTable = _libraryDataSet.GetReservations();
            DataRow row = reservationsTable.AsEnumerable().FirstOrDefault(r => (int)r["ReservationId"] == reservationId);
            if (row != null)
            {
                row.Delete();
                _libraryDataSet.SaveChanges();
            }
        }

        public int GetReservationCountForBook(int bookId)
        {
            // Ottieni la tabella delle prenotazioni
            DataTable reservationsTable = _libraryDataSet.GetReservations();

            // Conta il numero di prenotazioni per il libro specificato
            int reservationCount = 0;
            foreach (DataRow row in reservationsTable.Rows)
            {
                if ((int)row["BookId"] == bookId)
                {
                    reservationCount++;
                }
            }

            return reservationCount;
        }
    }
}
