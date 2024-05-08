using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BookCRUD
    {
        private LibraryDataSet _libraryDataSet;

        public BookCRUD()
        {
            _libraryDataSet = new LibraryDataSet();
        }
        public void AddBook(Book book)
        {
            DataTable booksTable = _libraryDataSet.GetBooks();
            DataRow newRow = booksTable.NewRow();
            newRow["Title"] = book.Title;
            newRow["AuthorName"] = book.AuthorName;
            newRow["AuthorSurname"] = book.AuthorSurname;
            newRow["PublishingHouse"] = book.PublishingHouse;
            newRow["Quantity"] = book.Quantity;
            booksTable.Rows.Add(newRow);
            _libraryDataSet.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            DataTable booksTable = _libraryDataSet.GetBooks();
            DataRow row = booksTable.AsEnumerable().FirstOrDefault(r => (int)r["BookId"] == book.BookId);
            if (row != null)
            {
                row["Title"] = book.Title;
                row["AuthorName"] = book.AuthorName;
                row["AuthorSurname"] = book.AuthorSurname;
                row["PublishingHouse"] = book.PublishingHouse;
                //row["Quantity"] = book.Quantity;
                _libraryDataSet.SaveChanges();
            }
        }
        public void DeleteBook(Book book)
        {
            DataTable booksTable = _libraryDataSet.GetBooks();
            DataRow row = booksTable.AsEnumerable().FirstOrDefault(r => (int)r["BookId"] == book.BookId);
            if (row != null)
            {
                row.Delete();
                _libraryDataSet.SaveChanges();
            }
        }

        public void IncrementBookQuantity(string title)
        {
            DataTable booksTable = _libraryDataSet.GetBooks();
            DataRow row = booksTable.AsEnumerable().FirstOrDefault(r => (string)r["Title"] == title);
            if (row != null)
            {
                int currentQuantity = (int)row["Quantity"];
                row["Quantity"] = currentQuantity + 1;
                _libraryDataSet.SaveChanges();
            }
        }

        public DataTable GetBooksTable()
        {
            return _libraryDataSet.GetBooks();
        }

    }
}
