using System;
using System.Data;
using System.IO;
using Model;

namespace DAL
{
    public class LibraryDataSet
    {
        private readonly string _dataSetFilePath = "library_data.xml"; // Unico file per l'intero DataSet

        private DataSet _libraryDataSet;

        public LibraryDataSet()
        {
            InitializeLibraryDataSet();
        }

        private void InitializeLibraryDataSet()
        {
            _libraryDataSet = new DataSet("LibraryDataSet");

            if (File.Exists(_dataSetFilePath))
            {
                _libraryDataSet.ReadXml(_dataSetFilePath);
            }
            else
            {
                InitializeBooksTable();
                InitializeUsersTable();
                InitializeReservationsTable();
                _libraryDataSet.WriteXml(_dataSetFilePath, XmlWriteMode.WriteSchema);
            }
        }

        private void InitializeBooksTable()
        {
            DataTable booksTable = new DataTable("Books");
            booksTable.Columns.Add("BookId", typeof(int)).AutoIncrement = true;
            booksTable.Columns.Add("Title", typeof(string));
            booksTable.Columns.Add("AuthorName", typeof(string));
            booksTable.Columns.Add("AuthorSurname", typeof(string));
            booksTable.Columns.Add("PublishingHouse", typeof(string));
            booksTable.Columns.Add("Quantity", typeof(int));
            booksTable.PrimaryKey = new DataColumn[] { booksTable.Columns["BookId"] };
            _libraryDataSet.Tables.Add(booksTable);
        }

        private void InitializeUsersTable()
        {
            DataTable usersTable = new DataTable("Users");
            usersTable.Columns.Add("UserId", typeof(int)).AutoIncrement = true;
            usersTable.Columns.Add("Username", typeof(string));
            usersTable.Columns.Add("Password", typeof(string));
            usersTable.Columns.Add("Role", typeof(string));
            usersTable.PrimaryKey = new DataColumn[] { usersTable.Columns["UserId"] };
            _libraryDataSet.Tables.Add(usersTable);
        }

        private void InitializeReservationsTable()
        {
            DataTable reservationsTable = new DataTable("Reservations");
            reservationsTable.Columns.Add("ReservationId", typeof(int)).AutoIncrement = true;
            reservationsTable.Columns.Add("UserId", typeof(int));
            reservationsTable.Columns.Add("BookId", typeof(int));
            reservationsTable.Columns.Add("StartDate", typeof(DateTime));
            reservationsTable.Columns.Add("EndDate", typeof(DateTime));
            reservationsTable.PrimaryKey = new DataColumn[] { reservationsTable.Columns["ReservationId"] };
            _libraryDataSet.Tables.Add(reservationsTable);
        }

        public DataTable GetBooks()
        {
            return _libraryDataSet.Tables["Books"];
        }

        public DataTable GetUsers()
        {
            return _libraryDataSet.Tables["Users"];
        }

        public DataTable GetReservations()
        {
            return _libraryDataSet.Tables["Reservations"];
        }

        public void SaveChanges()
        {
            _libraryDataSet.WriteXml(_dataSetFilePath, XmlWriteMode.WriteSchema);
        }

        // Metodo per la stampa di una DataTable
        public void PrintDataTable(DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                foreach (DataColumn col in dataTable.Columns)
                {
                    Console.Write(row[col] + "\t");
                }
                Console.WriteLine();
            }
        }
        // Leggere un dataset
        public void ReadDataSet()
        {
            // Lettura e visualizzazione delle tabelle
            DataTable booksTable = GetBooks();
            DataTable usersTable = GetUsers();
            DataTable reservationsTable = GetReservations();

            Console.WriteLine("Books:");
            PrintDataTable(booksTable);

            Console.WriteLine("\nUsers:");
            PrintDataTable(usersTable);

            Console.WriteLine("\nReservations:");
            PrintDataTable(reservationsTable);
        }

    }
}
