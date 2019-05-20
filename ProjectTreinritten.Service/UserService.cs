using ProjectTreinritten.Domain.Entities;
using ProjectTreinritten.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTreinritten.Service
{
    public class UserService
    {
        private UserDAO userDAO;

        public UserService()
        {
            userDAO = new UserDAO();
        }

        public IEnumerable<AspNetUsers> GetAll()
        {
            return userDAO.GetAll();
        }

        public AspNetUsers Get(string id)
        {
            return userDAO.Get(id);
        }

        public void Update(AspNetUsers entity)
        {
            userDAO.Update(entity);
        }

        public void Create(AspNetUsers entity)
        {
            userDAO.Create(entity);
        }
    }
}
