
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
    public class LoginManager
    {
        private readonly UserCRUD _userCRUD;
        private readonly CurrentUserManager _currentUserManager;

        public LoginManager()
        {
            _userCRUD = new UserCRUD();
            _currentUserManager = CurrentUserManager.Instance;
        }

        public (int userId, UserRole role, string statusMessage) CheckLogin(string username, string password)
        {
            var usersTable = _userCRUD.GetUsersTable();
            var userRow = usersTable.AsEnumerable().FirstOrDefault(row => row.Field<string>("Username") == username);
            if (userRow == null)
            {
                return (-1, UserRole.User, "Utente non trovato.");
            }
            if (userRow.Field<string>("Password") == password)
            {
                int userId = userRow.Field<int>("UserId");
                UserRole role = (UserRole)Enum.Parse(typeof(UserRole), userRow.Field<string>("Role"));

                // Imposta l'utente corrente nel CurrentUserManager
                _currentUserManager.SetCurrentUser(userId);

                // Restituisci l'ID dell'utente, il ruolo e il messaggio di stato
                return (userId, role, "Login riuscito.");
            }
            else
            {
                return (-1, UserRole.User, "Password errata.");
            }
        }
    }

}
