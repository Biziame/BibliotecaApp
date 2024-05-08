using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DAL
{
    public class UserCRUD
    {
        private LibraryDataSet _libraryDataSet;

        public UserCRUD()
        {
            _libraryDataSet = new LibraryDataSet();
        }

        public void AddUser(User user)
        {
            DataTable usersTable = _libraryDataSet.GetUsers();
            DataRow newRow = usersTable.NewRow();
            newRow["Username"] = user.Username;
            newRow["Password"] = user.Password;
            newRow["Role"] = user.Role;
            usersTable.Rows.Add(newRow);
            _libraryDataSet.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            DataTable usersTable = _libraryDataSet.GetUsers();
            DataRow row = usersTable.AsEnumerable().FirstOrDefault(r => (int)r["UserId"] == user.UserId);
            if (row != null)
            {
                row["Username"] = user.Username;
                row["Password"] = user.Password;
                row["Role"] = user.Role;
                _libraryDataSet.SaveChanges();
            }
        }

        public void DeleteUser(User user)
        {
            DataTable usersTable = _libraryDataSet.GetUsers();
            DataRow row = usersTable.AsEnumerable().FirstOrDefault(r => (int)r["UserId"] == user.UserId);
            if (row != null)
            {
                row.Delete();
                _libraryDataSet.SaveChanges();
            }
        }

        public DataTable GetUsersTable()
        {
            return _libraryDataSet.GetUsers();
        }

    }
}
