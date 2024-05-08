using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UserService
    {
        private readonly UserCRUD _userCRUD;

        public UserService()
        {
            _userCRUD = new UserCRUD();
        }

        public void AggiuntaUtente(User user)
        {
            _userCRUD.AddUser(user);
        }

        public void AggiornaUtente(User user)
        {
            _userCRUD.UpdateUser(user);
        }

        public void EliminaLibro(User user)
        {
            _userCRUD.DeleteUser(user);
        }


    }
}
