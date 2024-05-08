using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Reservation
{
    public int ReservationId { get; set; }
    public int UserId { get; set; }
    public int BookId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    // Costruttore personalizzato per creare una nuova prenotazione con data di fine calcolata automaticamente
    public Reservation(int userId, int bookId, DateTime startDate)
    {
        UserId = userId;
        BookId = bookId;
        StartDate = startDate;
        EndDate = startDate.AddDays(30); // Calcola la data di fine prenotazione (es. 30 giorni dopo la data di inizio)
    }
}
