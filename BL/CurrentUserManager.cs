using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CurrentUserManager
    {
        private static CurrentUserManager _instance;
        private int _userId;
        private DateTime _loginTime;

        // Proprietà per accedere all'utente corrente e all'orario di login
        public int UserId => _userId;
        public DateTime LoginTime => _loginTime;

        // Metodo per ottenere l'istanza singleton
        public static CurrentUserManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CurrentUserManager();
                }
                return _instance;
            }
        }

        // Metodo per impostare l'utente corrente e l'orario di login
        public void SetCurrentUser(int userId)
        {
            _userId = userId;
            _loginTime = DateTime.Now;
        }
    }
}
